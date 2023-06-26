sleep 90s

/opt/mssql-tools/bin/sqlcmd -S localhost,1433 -U SA -P "MeuDB@123" -i script.sql