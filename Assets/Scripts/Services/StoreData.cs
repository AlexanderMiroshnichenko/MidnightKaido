using System.Collections;
using System.Collections.Generic;
using UnityEngine.Purchasing;
using UnityEngine;


[CreateAssetMenu(fileName = "StoreData", menuName = "Scriptable Objects/StoreData")]
public class StoreData : ScriptableObject
{
    [System.Serializable]
    public class Product
    {
public string id;
public string androidId;
public string iosId;

public ProductType productType = ProductType.Consumable;

public string GetStoreId(){
    return id;
}
    }

    public List<Product> products;

    public Product GetProduct(string id){
        return products.Find(x=>x.id==id);
    }

}
