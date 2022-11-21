## Auth

### Register
```js
POST {{host}}/v1/auth/register
```

### Register Request
```json
{
	"firstname": "Francis",
	"lastname": "Klin",
	"email": "franklin1954@domain.com",
	"password": "Password123!"
}
```

### Register Response
```js
200 OK
```
```json
{
    "id": "d81c8703-9872-428f-8be4-96a7cdccbca6",
    "firstname": "Francis",
    "lastname": "Klin",
    "email": "franklin1954@domain.com",
    "token": "eyb...ajdjf"
}
```

### Login
```js
POST {{host}}/v1/auth/login
```

### Login Request
```json
{
    "email": "franklin1954@domain.com",
    "password": "Password123!"
}
```

### Login Response
```js
200 OK
```
```json
{
    "id": "d81c8703-9872-428f-8be4-96a7cdccbca6",
    "firstname": "Francis",
    "lastname": "Klin",
    "email": "franklin1954@domain.com",
    "token": "eyb...ajdjf"
}
```