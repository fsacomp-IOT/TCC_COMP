# API TCC_2020

## Dashboard

| Atividade | Status |
| - | - |
| Device | OK |
| Device_Data | OK |
| Plants | Working |

## Rotas

### Device

- **GET (/api/devices/{Device_ID})**

	Response (200):
	```
	{
	    "id": "T35T3CCC0MP",
	    "name": "Novo Jardim",
	    "created_at": "25/07/2020 11:53:31",
	    "updated_at": "25/07/2020 11:53:31",
	    "connected": "Desconectado",
	    "deviceData": {
	      "id": 2,
	      "soil_humidity": 1.5,
	      "air_humidity": 2,
	      "air_temperature": 25.5,
	      "solar_light": 10,
	      "created_at": "25/07/2020 12:05:02",
	      "device_id": "T35T3"
	    }
	}
	```
	
- **GET ALL (/api/devices)**

	Response (200):
	```
	[
	    {
            "id": "T35T3CCC0MP",
	        "name": "Novo Jardim",
	        "created_at": "25/07/2020 11:53:31",
	        "updated_at": "25/07/2020 11:53:31",
	        "connected": "Desconectado",
	        "deviceData": {
	            "id": 2,
	            "soil_humidity": 1.5,
	            "air_humidity": 2,
	            "air_temperature": 25.5,
	            "solar_light": 10,
	            "created_at": "25/07/2020 12:05:02",
	            "device_id": "T35T3"
	        }
	    }
	]
	```
	  
- **POST (/api/devices)**

  	Request:
  	```
	{
	    "id": "T35T7",
	    "name": "Novo Jardim",
	    "deviceData": {
	        "soil_humidity": 7.8,
	        "air_humidity": 10,
	        "air_temperature": 30.0,
	        "solar_light": 9
	    }
	}
	```
	
	Response (200):
	```
	true
	```
  	
- **PUT (/api/devices/{Device_ID})**

	Request:
	```
	{
	    "name": "Jardinagem"
	}
	```
	
	Response (200):
	```
	true
	```
	
- **DELETE (/api/devices/{Device_ID})**

	```
	true
	```
