var explosion : Transform;

function OnTriggerEnter( hit : Collider )
{ 
     
     if(hit.gameObject.== "Player")
     {
     
      Destroy(hit.gameObject);
      var exp = Instantiate(explosion, gameObject.transform.position,Quaternion.identity );
     
      Destroy(gameObject);
      
     }


}