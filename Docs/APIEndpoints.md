# Users:

| Action      | REST Action | Endpoint                | Parameters Source | Auth. | Require Admin | Admin Is Allowed |
| ----------- | ----------- | ----------------------- | ----------------- | ----- | ------------- | ---------------- |
| Create      | Post        | /api/users              | FromBody          | No    | No            |                  |
| Read        | Get         | /api/users              | FromQuery         | Yes   | No            |                  |
| ReadByEmail | Get         | /api/users              | FromQuery         | Yes   | Yes           |                  |
| ReadById    | Get         | /api/users/{id}         |                   | Yes   | No            |                  |
| Update      | Put         | /api/users/update/{id}  | FromQuery         | Yes   | No            |                  |
| Delete      | Delete      | /api/users/{id}         |                   | Yes   | No            | Yes              |
| Promote     | Put         | /api/users/promote/{id} |                   | Yes   | Yes           |                  |
| Demote      | Put         | /api/users/demote/{id}  |                   | Yes   | Yes           |                  |
| Block       | Put         | /api/users/block/{id}   |                   | Yes   | Yes           |                  |
| Unblock     | Put         | /api/users/unblock/{id} |                   | Yes   | Yes           |                  |
| Login       | ???         | /api/users/login        | FromHeader        | No    | No            |                  |
| Logout      | ???         | /api/users/logout       | FromHeader        | Yes   | No            |                  |

<br>

# Threads:

| Action          | REST Action | Endpoint                  | Parameters Source                   | Auth. | Require Admin | Admin Is Allowed |
| --------------- | ----------- | ------------------------- | ----------------------------------- | ----- | ------------- | ---------------- |
| Create          | Post        | /api/threads              | FromBody                            | Yes   | No            | Yes              |
| Read            | Get         | /api/threads              | FromQuery                           | No    | No            |                  |
| Update          | Put         | /api/threads/update/{id}  | FromQuery (+tags)                   | Yes   | No            |                  |
| Delete          | Delete      | /api/threads/{id}         |                                     | Yes   | Yes           | Yes              |
| UpVote          | Put         | /api/threads/upvote/{id}  |                                     | Yes   | No            |                  |
| DownVote        | Put         | /api/threads/dowvote/{id} |                                     | Yes   | No            |                  |
| Public Endpoint | Get         | /api/public               | Hardcoded top 10 recent, commented. | No    | No            |                  |

<br>

# Replies:

| Action   | REST Action | Endpoint                   | Parameters Source | Auth. | Require Admin | Admin Is Allowed |
| -------- | ----------- | -------------------------- | ----------------- | ----- | ------------- | ---------------- |
| Create   | Post        | /api/replies               | FromBody          | Yes   | No            | Yes              |
| Read     | Get         | /api/replies               | FromQuery         | No    | No            |                  |
| Update   | Put         | /api/replies/update/{id}   | FromQuery         | Yes   | No            |                  |
| Delete   | Delete      | /api/replies/{id}          |                   | Yes   | Yes           | Yes              |
| UpVote   | Put         | /api/replies/upvote/{id}   |                   | Yes   | No            |                  |
| DownVote | Put         | /api/replies/downvote/{id} |                   | Yes   | No            |                  |
