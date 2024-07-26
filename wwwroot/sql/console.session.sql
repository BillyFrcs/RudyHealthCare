SELECT * FROM Patients;

SELECT * FROM Admin;

DELETE FROM Admin WHERE Username = 'billyfrcs';

DROP TABLE IF EXISTS __EFMigrationsHistory;

DROP TABLE IF EXISTS Admin, Patients, __EFMigrationsHistory,
AspNetRoleClaims, AspNetRoles, AspNetUserClaims, AspNetUserLogins, AspNetUserRoles, AspNetUsers, AspNetUserTokens;