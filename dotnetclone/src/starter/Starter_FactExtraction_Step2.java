package starter;
import publisher.StarterPublisher;
import configuration.Configuration;
import simhash.SimhashCloneTest6e;;



public class Starter_FactExtraction_Step2 {

	/**
	 * @param args
	 * @throws Exception 
	 * Push test
	 */
	public static void main(String[] args) throws Exception {
		
		Configuration config=Configuration.loadFromFile();
		StarterPublisher.start(config);
		SimhashCloneTest6e.findClone(config.disassebledAddress+"\\ConsoleApplication3.exe_2_source.xml");
	}
	
	
	

}
