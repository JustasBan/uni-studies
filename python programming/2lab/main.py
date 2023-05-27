import zipfile
import json
import pprint
from bson import Decimal128
from pymongo import MongoClient

# --------------------------------------------
# idedam suzipuotus duomenis i mongoDB

def unzip(file_path='restaurants.zip'):
    with zipfile.ZipFile(file_path, 'r') as zip_ref:
        zip_ref.extractall()

def connectToDB(clientUrl='mongodb://localhost:27017/', db_name='python_lab2', collection_name='mokslai'):
        client = MongoClient(clientUrl)
        db = client[db_name]
        collection = db[collection_name]
        return collection, client

def pasiruostiDB():
    unzip()

    collection, client = connectToDB()

    if collection != None:
        print("Connected to MongoDB")
    else:
        print("Connection failed")

    # Load data from JSON file
    with open('retaurants.json') as f:
        data = []
        for line in f:
            data.append(json.loads(line))

    # isvalom db (kad nesikartotu duomenys)
    collection.delete_many({})

    # uzpildom db
    result = collection.insert_many(data)

    client.close()
    print("Disconnected from MongoDB")
    
    print(f"Inserted {len(result.inserted_ids)} documents into the collection.")

def visiDokumentai():
    collection, client = connectToDB()

    results = list(collection.find())        
    
    client.close()

    return results

def laukai1():
    collection, client = connectToDB()

    results = list(collection.find({}, {'restaurant_id': 1, 'name': 1, 'borough': 1, 'cuisine': 1}))        
    
    client.close()

    return results

def laukai2():
    collection, client = connectToDB()

    # "field_id" atitikmuo yra "_id" mano lokalioje db
    results = list(collection.find({}, {'_id': 0, 'restaurant_id': 1, 'name': 1, 'borough': 1, 'cuisine': 1}))        
    
    client.close()

    return results

def bronxRestoranai():
    collection, client = connectToDB()

    results = list(collection.find({"borough": "Bronx"}))

    client.close()

    return results

def ivertinimai():
    collection, client = connectToDB()

    query = [
        {"$match": {"grades.score": {"$gte": Decimal128('80'), "$lte": Decimal128('100')}}},
        {"$project": {"_id": 0, "restaurant_id": 1, "name": 1, "borough": 1, "cuisine": 1}},
    ]

    results = list(collection.aggregate(query))

    client.close()

    return results

def rikiavimas():
    collection, client = connectToDB()

    query = {}

    sort_criteria = [("cuisine", 1), ("borough", -1)]

    results = list(collection.find(query, {"_id": 0, "restaurant_id": 1, "name": 1, "borough": 1, "cuisine": 1}).sort(sort_criteria))

    client.close()

    return results


def jsonRezulatai(results, filename):
    with open(filename, 'w') as f:
        json.dump(results, f, default=str)
# --------------------------------------------
# uzduotys

# 1 uzduotis
pasiruostiDB()

# 2 uzduotis
jsonRezulatai(visiDokumentai(), "rezultatai/visiDokumentai.json")

# 3 uzduotis
jsonRezulatai(laukai1(), "rezultatai/laukai1.json")

# 4 uzduotis
jsonRezulatai(laukai2(), "rezultatai/laukai2.json")

# 5 uzduotis
jsonRezulatai(bronxRestoranai(), "rezultatai/bronxRestoranai.json")

# 6 uzduotis
jsonRezulatai(ivertinimai(), "rezultatai/ivertinimai.json")

# 7 uzduotis
jsonRezulatai(rikiavimas(), "rezultatai/rikiavimas.json")
