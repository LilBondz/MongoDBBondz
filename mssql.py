import pymssql

server = "192.168.15.15\\IT22"
user = "sa"
password = "123QWEr"

connect = pymssql.connect(server, user, password, "datob")
exe = connect.cursor()
exe.execute("IF OBJECT_ID('ansatt', 'U') IS NULL CREATE TABLE ansatt (ansattnr INT NOT NULL IDENTITY PRIMARY KEY, fornavn VARCHAR(100), etternavn VARCHAR(100), PRIMARY KEY(id))")
running = True
id
while running:
    fornavn = input("Skriv inn fornavn: ")
    etternavn = input("Skriv inn etternavn: ")
    exe.execute(f"INSERT INTO ansatt VALUES ({fornavn}, {etternavn}")
    again = input("Legg til en til? y/n")
    if again == "y":
        pass
    else:
        running = False

connect.commit()

