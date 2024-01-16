
# Tresure_api

---

## Account

### Login
- **Method**: POST
- **Path**: /api/login
- **Body**:
```
{
  "email": "user@example.com",
  "password": "string"
}
```
- **Response Example**:
```
{
  "email": "string",
  "username": "string",
  "token": "string"
}
```

### Get User
- **Method**: GET
- **Path**: /api/user
- **Body**:
```
/api/user?userEmail=test@test.com
```
- **Response Example**:
```
{
  "id": "string",
  "email": "string",
  "username": "string"
}
```

### Register
- **Method**: POST
- **Path**: /api/register
- **Body**:
```
{
  "username": "string",
  "email": "user@example.com",
  "password": "string",
  "repeatPassword": "string"
}
```
- **Response Example**:
```
{
  "id": "string",
  "email": "string",
  "username": "string"
}
```

### Get Current User
- **Method**: GET
- **Path**: /api/currentUser

- **Response Example**:
```
{
  "email": "string",
  "username": "string",
  "token": "string"
}
```

---

## Card

### Get Card
- **Method**: GET
- **Path**: /api/Card/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
{
  "id": 0,
  "title": "string",
  "details": "string",
  "tags": "string",
  "dueDate": "2024-01-16T12:33:31.404Z",
  "creationDate": "2024-01-16T12:33:31.404Z",
  "completionDate": "2024-01-16T12:33:31.404Z",
  "approvalDate": "2024-01-16T12:33:31.404Z",
  "assignedMembers": [
    {
      "id": 0
    }
  ]
}

```

### Edit Card
- **Method**: PUT
- **Path**: /api/Card/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Body**:
```
{
  "id": 0,
  "title": "string",
  "details": "string",
  "tags": "string",
  "dueDate": "2024-01-16T12:35:08.628Z",
  "creationDate": "2024-01-16T12:35:08.628Z",
  "completionDate": "2024-01-16T12:35:08.628Z",
  "approvalDate": "2024-01-16T12:35:08.628Z",
  "assignedMembers": [
    {
      "id": 0
    }
  ],
  "columnId": 0
}
```
- **Response Example**:
```
200
```


### Delete Card
- **Method**: DELETE
- **Path**: /api/Card/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
200
```

### Create Card
- **Method**: POST
- **Path**: /api/Card
- **Headers**:
  - `Authorization: Bearer {token}`
- **Body**:
```
{
  "title": "string",
  "details": "string",
  "tags": "string",
  "dueDate": "2024-01-16T12:38:52.385Z",
  "creationDate": "2024-01-16T12:38:52.385Z",
  "completionDate": "2024-01-16T12:38:52.385Z",
  "approvalDate": "2024-01-16T12:38:52.385Z",
  "assignedMembers": [
    {
      "id": 0
    }
  ],
  "columnId": 0
}
```
- **Response Example**:
```
200
```

---


## Column

### Create a column
- **Method**: POST
- **Path**: /api/Column?project_id={int}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
200 / 404
```

### Edit Column Title
- **Method**: PUT
- **Path**: /api/Column/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Body**:
```
{
  "id": 0,
  "title": "string",
  "position": 0
}
```
- **Response Example**:
```
200
```

### Delete Column
- **Method**: DELETE
- **Path**: /api/Column/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
200
```

---

## Member

### Create a member
- **Method**: POST
- **Path**: /api/Member
- **Headers**:
  - `Authorization: Bearer {token}`
- **Body**:
```
{
  "userId": "string",
  "projectId": 0
}
```
- **Response Example**:
```
200
```

### Edit Change Member's Role
- **Method**: PUT
- **Path**: /api/Member/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Body**:
```
{
  "id": 0,
  "role": 0
}
```
- **Response Example**:
```
200
```

### Delete a Member
- **Method**: DELETE
- **Path**: /api/Member/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
200
```

---

## Project

### Send Project Update to all Sockets
- **Method**: POST
- **Path**: /api/Project/UpdateProject
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
200
```

### Get Project Data
- **Method**: GET
- **Path**: /api/Project/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
{
  "id": 0,
  "title": "string",
  "columns": [
    {
      "id": 0,
      "title": "string",
      "cards": [
        {
          "id": 0,
          "title": "string",
          "details": "string",
          "tags": "string",
          "dueDate": "2024-01-16T12:54:02.003Z",
          "creationDate": "2024-01-16T12:54:02.003Z",
          "completionDate": "2024-01-16T12:54:02.003Z",
          "approvalDate": "2024-01-16T12:54:02.003Z",
          "assignedMembers": [
            {
              "id": 0
            }
          ]
        }
      ],
      "position": 0
    }
  ],
  "members": [
    {
      "id": 0,
      "user": {
        "id": "string",
        "email": "string",
        "username": "string"
      },
      "roles": [
        {
          "role": {
            "name": 0
          }
        }
      ]
    }
  ]
}
```

### Rename a Project
- **Method**: PUT
- **Path**: /api/Project/{id}/?projectTitle=newTitle
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
200
```

### Delete a Project
- **Method**: DELETE
- **Path**: /api/Project/{id}
- **Headers**:
  - `Authorization: Bearer {token}`
- **Response Example**:
```
200
```