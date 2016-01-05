//Allows versions of QR codes to be modified to the most recent or most usable version.
public static class BoardGameMediator{
	//The old format is = Shelf #<A>, <B>, <C>, <D>, <E> - <F> players, <G> minutes, <H> to learn
	public const string 
	    QR_CODE_FORMAT_V1 = "Shelf #{0}, {1}, {2}, {3}, {4} - {5}, {6} minutes, {7} to learn",
	    QR_CODE_FORMAT_V2; //Unfortunately, I don't have the new Format flushed out yet, it will either be the ID or a list of quialities similar to the last.

    //I hope to use a generic convert to method, but for now, hardcoding is best.
    //public static void convertTo(); 
    //Follows a similiar pattern to searching the database.
    public static string buildQrCodeV1(int shelf = -1, string name = null, string rating = null, string type = null, int minPlayers = -1, int minPlayers = -1, string length = null, string difficulty = null){
        String code = QR_CODE_FORMAT_V1;
        //We could also do string format; which seems like a decent idea.
        //We could also just build the string backwards, by insertion, or append to the end of another string... think about it.
        return String.Format(QR_CODE_FORMAT_V1,shelf,name,rating,type,minPlayers,maxPlayers,length,difficulty);
    }

}

