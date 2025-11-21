-- Update zyp user to have admin role
UPDATE users 
SET role = 'admin' 
WHERE username = 'zyp';

-- Verify the update
SELECT username, role FROM users WHERE username = 'zyp';
