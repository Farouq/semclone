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
		
		for(int i=1;i<=25;i++){
		filters.Filter1.filter1(config.disassebledAddress+"\\f ("+i+").xml");
		filters.Filter4.filter4(config.disassebledAddress+"\\f ("+i+")_f1.xml");
		filters.Filter2.filter2(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		filters.Filter4.filter5(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		filters.Filter4.filter6(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		filters.Filter4.filter7(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		filters.Filter3.filter3(config.disassebledAddress+"\\f ("+i+")_f1_4.xml");
		
		System.out.println(" file number iiiii"+i+"------------------------------------------------------"+i);
		//SimhashCloneTest6e.findClone(config.disassebledAddress+"\\F ("+i+")_f1_f4.xml");
		}
		
		
		//SimhashCloneTest6e.findClone(config.disassebledAddress+"\\chap (15).xml");
		
		//filters.Filter1.filter(config.disassebledAddress+"\\chap (1).xml");
		//filters.Filter2.filter(config.disassebledAddress+"\\chap (1)_filtered2.xml");
		//filters.Filter3.filter(config.disassebledAddress+"\\chap (1)_filtered2_filtered2.xml");
		//filters.Filter4.filter(config.disassebledAddress+"\\chap (1)_filtered2_filtered2.xml");
		
	}
	
	
	

}
