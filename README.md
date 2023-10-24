
finiished at register page start,
smith tutorial doesnt have JWT -> use neil's or others for jwt
login post looks similar like neil's
next up: finish smith


---

all
/api/Cards
/api/Columns
/api/Member
/api/Projects
/api/Roles
/api/Users
/api/Account/login
/api/Account/register

---
actually needed routes
/api/Projects
/api/Cards
/api/Columns
/api/Member
/api/login
/api/register

---

/api/Projects               GET           list      200
/api/Projects/{project_id}  GET           one       200
/api/Projects/              POST          create    201
/api/Projects/{project_id}  PUT/PATCH     edit      200
/api/Projects/{project_id}  DELETE        remove    404/204

---
GET

id
title
columns
    id
    title
    position
    cards
        id
        title
        details
        tags
        dueDate
        creationDate
        completionDate
        assignedMembers
            userId? (when displaying on card get username from members)
members
    userId
    username
    email
    roles
        roleId
        roleName


---
POST

title
(in api) members {userId, projectId, roles: admin}

---

/api/Projects/{id}/Cards                  GET           list      200

/api/Cards/{card_id}        GET           single    200
/api/Cards/                 POST          create    201
/api/Cards/{card_id}        PUT/PATCH     edit      200
/api/Cards/{card_id}        DELETE        remove    404/204


---
GET

id
title
details
tags
dueDate
creationDate
isComplete
assignedMembers
    userId? (when displaying on card get username from members)


---
POST

title
details
tags
duedate
creationDate(in api probably)
isComplete(in api, false)
assignedMembers
    userId
columnId

---

/api/Projects/{id}/Columns                GET           list      200

/api/Columns/{column_id}    GET           single    200
/api/Columns                POST          create    201
/api/Columns/{column_id}    PUT/PATCH     edit      200
/api/Columns/{column_id}    DELETE        remove    404/204


---
GET

id
title
position
cards
    id
    title
    details
    tags
    dueDate
    creationDate
    isComplete
    assignedMembers
        userId? (when displaying on card get username from members)


---
POST

title
position

---
/api/Projects/{id}/Member    GET           list      200
/api/Member/{member_id}     GET           single    200
/api/Member                 POST          create    201
/api/Member/{member_id}     PUT/PATCH     edit      200
/api/Member/{member_id}     DELETE        remove    404/204

dotnet commands:

dotnet ef migrations add YourMigrationName
dotnet ef database update
dotnet watch --no-hot-reload


---
GET

userId
username
email
roles
    name


---
POST

userId
projectId
roles
    id

---
/api/Login                  POST          create    200
/api/Register               POST          create    200
/api/currentUser            POST          create    200


---
GET (currentUser)

id?
email
username