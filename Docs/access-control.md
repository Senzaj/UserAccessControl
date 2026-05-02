Tables:
-
users:
- id;
- email;
- password_hash;
- first_name;
- last_name;
- is_active;
- created_at;

roles:
- id;
- name;
- description.

user_roles:
- user_id;
- role_id.

permissions:
- id;
- resource;
- action.

role_permissions:
- role_id;
- permission_id;

Tables links:
-
- users → roles (many to many);
- roles → permissions (many to many);

