package starter;
import publisher.StarterPublisher;
import configuration.Configuration;
import simhash.SimhashCloneTest6e;
import filters.Filter1;
import filters.Filter2;



public class Starter_FactExtraction_Step2 {

	/**
	 * @param args
	 * @throws Exception 
	 * Push test
	 */
	public static void main(String[] args) throws Exception {
		
		Configuration config=Configuration.loadFromFile();
		//StarterPublisher.start(config);
		/*
		for(int i=1;i<=22;i++){
		filters.Filter1.filter(config.disassebledAddress+"\\chap ("+i+").xml");
		System.out.println(" file number iiiii"+i+"------------------------------------------------------"+i);
		//SimhashCloneTest6e.findClone(config.disassebledAddress+"\\chap ("+i+")_filtered.xml");
		}
		
		*/
		//SimhashCloneTest6e.findClone(config.disassebledAddress+"\\chap (15).xml");
		
		//filters.Filter1.filter(config.disassebledAddress+"\\chap (1).xml");
		filters.Filter2.filter(config.disassebledAddress+"\\chap (1)_filtered2.xml");
		
	}
	
	
	

}
