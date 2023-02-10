
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

   public  class Question
	{
	static MongoClient client;
	static IMongoDatabase QuestionDatabase;
	static IMongoCollection<Question> QuestionCollection;
	public Question(string newDescription, string newAnswer, string category)
	{

		Description = newDescription;
		Answer = newAnswer;
		Category = category;
	}
	[BsonId]
	[BsonIgnoreIfDefault]
	ObjectId _id;

	public string Description { get; set; }
        public string Answer { get; set; }
	public string Category { get; set; }
	public void AddToDb()
	{
		client = new MongoClient();
		QuestionDatabase = client.GetDatabase("ViktorinaDb");
		QuestionCollection = QuestionDatabase.GetCollection<Question>("Questions");
		QuestionCollection.InsertOne(this);
	}
	public static List<Question> FindAllInDb()
	{
		client = new MongoClient();
		QuestionDatabase = client.GetDatabase("ViktorinaDb");
		QuestionCollection = QuestionDatabase.GetCollection<Question>("Questions");
		return QuestionCollection.Find(x => true).ToList();
	}
}

