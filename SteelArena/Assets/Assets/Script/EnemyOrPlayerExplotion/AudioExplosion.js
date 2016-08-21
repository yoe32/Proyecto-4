var soundFile:AudioClip;
 
 function OnTriggerEnter(trigger:Collider) {
      if(trigger.GetComponent.<Collider>().tag=="Player") {
         GetComponent.<AudioSource>().clip = soundFile;
         GetComponent.<AudioSource>().Play();
      }
 }