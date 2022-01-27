from pymongo import MongoClient
import pymongo

ansatt = {}


def get_database():
    CONNECTION_STRING = "mongodb+srv://bondz:123QWEr@cluster0.uterx.mongodb.net/myFirstDatabase?retryWrites=true&w=majority"
    client = MongoClient(CONNECTION_STRING)
    return client


def registrer_ansatt():
    global ansatt
    ansattnr = input("ansattnr: ")
    fornavn = input("fornavn: ")
    etternavn = input("etternavn: ")
    ansatt = {
        "ansattnr": ansattnr,
        "fornavn": fornavn,
        "etternavn": etternavn,
    }


if __name__ == '__main__':
    registrer_ansatt()
    client = get_database()
    print(client.list_database_names())
    mydb = client["db_bondz_ansatt"]
    mycol = mydb["ansattliste"]
    mydict = ansatt
    resultat = mycol.insert_one(mydict)
    print(resultat)
    print(client.list_database_names())
