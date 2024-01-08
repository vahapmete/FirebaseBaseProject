
using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Persistence.Repositories;

namespace Domain.Firebase;

[FirestoreData]
public class ProductFire : Entity
{
    [FirestoreProperty]
    public int? CategoryId { get; set; }
    [FirestoreProperty]
    public string BrandId { get; set; }
    [FirestoreProperty]
    public string Name { get; set; }
    [FirestoreProperty]
    public string ShortDescription { get; set; }
    [FirestoreProperty]
    public string LongDescription { get; set; }
    [FirestoreProperty]
    public double Price { get; set; }
    [FirestoreProperty]
    public int? Quantity { get; set; }

  


}