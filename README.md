OpenAPI file can be find at
```
http://localhost:5185/openapi/v1.json
```

curl -X POST http://localhost:5185/api/v1/products \
  -H "Content-Type: application/json" \
  -H "Authorization: Basic YWRtaW46c2VjcmV0" \
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a" \
  -d '{
    "name": "Gaming Mouse",
    "price": 49.99
}'


curl -i -X GET "http://localhost:5185/api/v1/products/by-price?minPrice=20&maxPrice=100" \
  -H "Authorization: Basic YWRtaW46c2VjcmV0" \
  -H "X-Tenant-Id: 330c1c10-0e36-47cb-b69b-985aec2af34a"