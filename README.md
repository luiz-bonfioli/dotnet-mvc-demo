# .NET MVC Demo

This project is a demonstration of a .NET MVC application featuring a multi-tenant architecture and a product management API.

## Features

- **Multi-Tenant Support**: Handles requests for different tenants using the `X-Tenant-Id` header.
- **Product Management API**: Provides endpoints to create, retrieve, update, and delete products.
- **OpenAPI Documentation**: Accessible at `http://localhost:5185/openapi/v1.json` for API exploration.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Running the Application

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/luiz-bonfioli/dotnet-mvc-demo.git
   cd dotnet-mvc-demo
   ```

2. **Restore Dependencies**:
   ```bash
   dotnet restore
   ```

3. **Build the Project**:
   ```bash
   dotnet build
   ```

4. **Run the Application**:
   ```bash
   dotnet run
   ```

   The application will start and listen on `http://localhost:5185/`.

## API Usage

### Authentication

All API requests require **Basic Auth**:
`Authorization: Basic YWRtaW46c2VjcmV0`

And must include the tenant header:
`X-Tenant-Id: <tenant-guid>`

---

### Create a New Product

```bash
curl -X POST http://localhost:5185/api/v1/products \
  -H "Content-Type: application/json"  \
  -H "Authorization: Basic YWRtaW46c2VjcmV0"  \
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a"  \
  -d '{ "name": "Gaming Mouse", "price": 49.99 }'
```

### Get All Products

```bash
curl -X GET http://localhost:5185/api/v1/products \
  -H "Authorization: Basic YWRtaW46c2VjcmV0" \
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a"
```

### Get Product By ID

```bash
curl -X GET http://localhost:5185/api/v1/products/{id}  \ 
  -H "Authorization: Basic YWRtaW46c2VjcmV0"  \
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a"
```

Example:

```bash
curl -X GET http://localhost:5185/api/v1/products/1  \ 
  -H "Authorization: Basic YWRtaW46c2VjcmV0" \  
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a"
```

### Get Products by Price Range

```bash
curl -X GET "http://localhost:5185/api/v1/products/by-price?minPrice=20&maxPrice=100" \
  -H "Authorization: Basic YWRtaW46c2VjcmV0" \
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a"
```

### Update a Product

```bash
curl -X PUT http://localhost:5185/api/v1/products \
  -H "Content-Type: application/json" \
  -H "Authorization: Basic YWRtaW46c2VjcmV0" \
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a" \
  -d '{ "id": 1, "name": "Updated Mouse", "price": 59.99 }'
```

### Delete a Product

```bash
curl -X DELETE http://localhost:5185/api/v1/products \
  -H "Content-Type: application/json" \
  -H "Authorization: Basic YWRtaW46c2VjcmV0" \
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a" \
  -d '{ "id": 1, "name": "Old Mouse", "price": 49.99 }'
```

## Project Structure

- `Domains/Products/`: Contains domain models and business logic related to products.
- `Infra/`: Infrastructure layer handling data access and external services.
- `Program.cs`: Entry point of the application configuring services and middleware.
- `appsettings.json`: Application configuration settings.

## Configuration

Ensure that the `appsettings.json` and `appsettings.Development.json` files are properly configured for your environment.

## 📊 Logging, Tracing & Metrics

The `.NET MVC Demo` application includes basic hooks for observability.

### ✅ Logging

- Uses `.NET ILogger` for structured logs
- Configurable in `appsettings.json`

### 📡 Tracing

- Supports OpenTelemetry
- Instrumentation for ASP.NET Core, HTTP clients
- Export to Jaeger, Zipkin, or OTLP

### 📈 Metrics

- Uses ASP.NET Core `EventCounters`
- OpenTelemetry integration for Prometheus

### 🌟 Recommendations

- Add Serilog for advanced logging
- Use OpenTelemetry for tracing and metrics
- Export data to tools like Grafana, Jaeger, or New Relic

## 👤 Author

Luiz Bonfioli
https://github.com/luiz-bonfioli