package starter;
import publisher.StarterPublisher;
import configuration.Configuration;



public class Starter_FactExtraction_Step2 {

	/**
	 * @param args
	 * @throws Exception 
	 * Push test
	 */
	public static void main(String[] args) throws Exception {
		
		Configuration config=Configuration.loadFromFile();
		StarterPublisher.start(config);
	}
	
	
	

}
