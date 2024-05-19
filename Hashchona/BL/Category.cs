namespace Hashchona.BL
{
    public class Category
    {
        int categoyID;
        string catName;
        int maxScore;

        public Category() { }
        public Category(int categoyID, string catName, int maxScore)
        {
            CategoyID = categoyID;
            CatName = catName;
            MaxScore = maxScore;
        }

        public int CategoyID { get => categoyID; set => categoyID = value; }
        public string CatName { get => catName; set => catName = value; }
        public int MaxScore { get => maxScore; set => maxScore = value; }
    }
}
