# Importerer SQL modulen til python
import pymssql

# Lager variabel for Tilkoblinksinfo
server = "192.168.15.15\\IT22"
# Brukernavnet vi logger inn i databasen med
user = "sa"
# Passordet til brukeren
password = "123QWEr"

# Variabel som kobler seg til SQL serveren med info variablene
connect = pymssql.connect(server, user, password, "dbtob")
# lager en variabel til styring av databasen
exe = connect.cursor()
# skriver inn tekskommandoer som blir kjørt på SQL serveren
# Den lager en database hvis den ikke er der med id, fornavn og etternavn kolonner
exe.execute("IF OBJECT_ID('ansatt', 'U') IS NULL CREATE TABLE ansatt (id INT NOT NULL IDENTITY PRIMARY KEY, fornavn VARCHAR(100), etternavn VARCHAR(100))")
# Spør bruker om å skrive fornavn
fornavn = input("Skriv inn fornavn: ")
# spør bruker om å skrive etternavn
etternavn = input("Skriv inn etternavn: ")

# Lager en variabel med kommandoen som skal kjøres på serveren med infoen
sql = f"INSERT INTO ansatt (fornavn, etternavn) VALUES ('{fornavn}', '{etternavn}')"
# printer den først i python for å vise at teksten som blir sent til servern er rett
print(sql)
# Kjører kommandoen på serveren
exe.execute(sql)
# Kjører commit som sier utfør endringer
connect.commit()

# Lukker broen mellom klient og server
connect.close()
