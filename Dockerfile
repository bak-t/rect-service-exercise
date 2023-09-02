ARG dotnet_version=7.0

# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:$dotnet_version AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY RectExercise.Application.Contract/*.csproj ./RectExercise.Application.Contract/
COPY RectExercise.Application.Implementation/*.csproj ./RectExercise.Application.Implementation/
COPY RectExercise.Application.Tests/*.csproj ./RectExercise.Application.Tests/
COPY RectExercise.Data.Contract/*.csproj ./RectExercise.Data.Contract/
COPY RectExercise.Data.Implementation.EF/*.csproj ./RectExercise.Data.Implementation.EF/
COPY RectExercise.Data.Tests/*.csproj ./RectExercise.Data.Tests/
COPY RectExercise.Domain.Contract/*.csproj ./RectExercise.Domain.Contract/
COPY RectExercise.Domain.Implementation/*.csproj ./RectExercise.Domain.Implementation/
COPY RectExercise.Domain.Tests/*.csproj ./RectExercise.Domain.Tests/
COPY RectExercise.WebApi.Host/*.csproj ./RectExercise.WebApi.Host/
RUN dotnet restore

# copy everything else and build app
COPY RectExercise.Application.Contract/. ./RectExercise.Application.Contract/
COPY RectExercise.Application.Implementation/. ./RectExercise.Application.Implementation/
COPY RectExercise.Application.Tests/. ./RectExercise.Application.Tests/
COPY RectExercise.Data.Contract/. ./RectExercise.Data.Contract/
COPY RectExercise.Data.Implementation.EF/. ./RectExercise.Data.Implementation.EF/
COPY RectExercise.Data.Tests/. ./RectExercise.Data.Tests/
COPY RectExercise.Domain.Contract/. ./RectExercise.Domain.Contract/
COPY RectExercise.Domain.Implementation/. ./RectExercise.Domain.Implementation/
COPY RectExercise.Domain.Tests/. ./RectExercise.Domain.Tests/
COPY RectExercise.WebApi.Host/. ./RectExercise.WebApi.Host/
# build & publish inside container
RUN dotnet publish -c release -o /app --no-restore ./RectExercise.WebApi.Host/RectExercise.WebApi.Host.csproj

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:$dotnet_version
WORKDIR /app
COPY --from=build /app ./

# appsettings customization
ENV ConnectionStrings__RectDb="Server=host.docker.internal;Initial Catalog=RectDb;User=Euclid;Password=password;TrustServerCertificate=True"

ENTRYPOINT ["dotnet", "RectExercise.WebApi.Host.dll"]
