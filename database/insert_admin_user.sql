INSERT INTO users (id, username, email, password_hash, role, is_active, created_at, updated_at)
VALUES (
  gen_random_uuid(), 
  'zyp', 
  'zyp@example.com', 
  'zyp123123', 
  'admin', 
  true, 
  NOW(), 
  NOW()
);
