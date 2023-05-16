# Use the official .NET Core SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env

# Set the working directory in the container
WORKDIR /app

# Copy the project files to the container
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0

# Set the working directory in the container
WORKDIR /app

# Copy the published output from the build environment to the container
COPY --from=build-env /app/out .

# Expose the port the application listens on
EXPOSE 5000

# Set the entry point for the container d
ENTRYPOINT ["dotnet", "Heartstone.dll"]
