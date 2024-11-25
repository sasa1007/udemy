# Use the official SQL Server image as the base
FROM mcr.microsoft.com/mssql/server:latest

# Switch to root user to install tools
USER root

# Install required dependencies
RUN apt-get update && \
    apt-get install -y curl gnupg2 apt-transport-https ca-certificates wget

# Download and add the Microsoft GPG key
RUN wget https://packages.microsoft.com/keys/microsoft.asc -O /tmp/microsoft.asc && \
    apt-key add /tmp/microsoft.asc

# Add the correct Microsoft repository for Debian/Ubuntu systems
RUN wget https://packages.microsoft.com/config/debian/10/prod.list -O /etc/apt/sources.list.d/mssql-prod.list

# Update the repository list
RUN apt-get update

# Install SQL Server tools (mssql-tools)
RUN apt-get install -y mssql-tools

# Clean up to reduce image size
RUN rm -rf /var/lib/apt/lists/*

# Set environment variable for SQL Server to prevent interactive prompts
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=YourStrongPassword123

# Expose port for SQL Server
EXPOSE 1433

# Set entrypoint command for SQL Server
CMD /opt/mssql/bin/sqlservr & \
    sleep 30 && \
    /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P 'MyStrongPassw0rd!' -Q "CREATE DATABASE udemy;" && \
    tail -f /dev/null
