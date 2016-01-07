//Allows versions of QR codes to be modified to the most recent or most usable version.
public static class BoardGameMediator{
	//The old format is = Shelf #<A>, <B>, <C>, <D>, <E> - <F> players, <G> minutes, <H> to learn
	public const string 
		QR_CODE_FORMAT_V1 = "Shelf #{0}, {1}, {2}, {3}, {4} - {5}, {6} minutes, {7} to learn",
		QR_CODE_FORMAT_V2 = "?"; //Unfortunately, I don't have the new Format flushed out yet, it will either be the ID or a list of quialities similar to the last.
	
	//I hope to use a generic convert to method, but for now, hardcoding is best.
	//public static void convertTo(); 
	//Follows a similiar pattern to searching the database.
	public static string buildQrCodeV1(int shelf = -1, string name = null, string rating = null, string type = null, int minPlayers = -1, int maxPlayers = -1, string Length = null, string difficulty = null){
		string code = QR_CODE_FORMAT_V1;
		//We could also do string format; which seems like a decent idea.
		//We could also just build the string backwards, by insertion, or append to the end of another string... think about it.
		return string.Format(code,shelf,name,rating,type,minPlayers,maxPlayers,Length,difficulty);
	}
	public static bool isValidQrCodeV1(string code){
		string[] search = { "Shelf #",", ",", ",", ",", "," - ",", "," minutes, "," to learn"};
		int 
			foundAt = -1,
			index = 0;
		bool failed = false;
		do{
			foundAt = code.IndexOf(search[index],foundAt+1);
			failed = foundAt == -1;
			index++;
		}while(!failed && index < search.Length); 
		return !failed;
	}
	
	public static QR_CODE_DATA stripQrCodeV1(string code){
		int 
			lastIndex = 0,
			nextIndex = 0;
		
		QR_CODE_DATA data = new QR_CODE_DATA ();
		
		
		//find shelf number
		string 
			lastSearch = "Shelf #",
			nextSearch = ", ";
		
		lastIndex = code.IndexOf(lastSearch) + lastSearch.Length; //We want the index at the end of this string
		nextIndex = code.IndexOf(nextSearch,lastIndex); //We want the index at the start of this string
		if(!int.TryParse(code.Substring(lastIndex,nextIndex - lastIndex),out data.shelf)){
			//We will use null and -1 as our defaults.
			data.shelf = -1;
		}
		
		//fix indexes and find name
		lastIndex = nextIndex + nextSearch.Length;
		nextSearch = ", ";
		nextIndex = code.IndexOf(nextSearch,lastIndex);
		
		data.name = code.Substring(lastIndex,nextIndex - lastIndex);
		
		//fix indexes and find rating
		lastIndex = nextIndex + nextSearch.Length;
		nextSearch = ", ";
		nextIndex = code.IndexOf(nextSearch,lastIndex);
		data.rating = code.Substring(lastIndex,nextIndex - lastIndex);
		
		//fix indexes and find type
		lastIndex = nextIndex + nextSearch.Length;
		nextSearch = ", ";
		nextIndex = code.IndexOf(nextSearch,lastIndex);
		data.type = code.Substring(lastIndex,nextIndex - lastIndex);
		
		//fix indexes and find minPlayers
		lastIndex = nextIndex + nextSearch.Length;
		nextSearch = " - ";
			nextIndex = code.IndexOf(nextSearch,lastIndex);
		if(!int.TryParse(code.Substring(lastIndex,nextIndex - lastIndex),out data.minPlayers)){
			//We will use null and -1 as our defaults.
			data.minPlayers = -1;
		}
		
		//fix indexes and find maxPlayers
		lastIndex = nextIndex + nextSearch.Length;
		nextSearch = ", ";
		nextIndex = code.IndexOf(nextSearch,lastIndex);
		if(!int.TryParse(code.Substring(lastIndex,nextIndex - lastIndex),out data.maxPlayers)){
			//We will use null and -1 as our defaults.
			data.maxPlayers = -1;
		}
		
		//fix indexes and find minutes
		lastIndex = nextIndex + nextSearch.Length;
		nextSearch = " minutes, ";
		nextIndex = code.IndexOf(nextSearch,lastIndex);
		data.minutes = code.Substring(lastIndex,nextIndex - lastIndex);
		
		
		//fix indexes and find difficulty
		lastIndex = nextIndex + nextSearch.Length;
		nextSearch = " to learn";
		nextIndex = code.IndexOf(nextSearch,lastIndex);
		data.difficulty = code.Substring(lastIndex,nextIndex - lastIndex);
		
		return data;
	}
	
}

//I'm probably going to replace this, and give it it's own file, for now, we'll leave it here.
[System.Serializable]
public class QR_CODE_DATA{
	public QR_CODE_DATA(){
		shelf = -1;
		minPlayers = -1;
		maxPlayers = -1;

		name = null;
		rating = null;
		type = null;
		minutes = null;
		difficulty = null;
	}
	public int 
		shelf,
		minPlayers,
		maxPlayers;
	public string 
		name,
		rating,
		type,
		minutes,
		difficulty;
};
