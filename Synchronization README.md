# Team5wga Synchronization
All synchronizable objects should be stored as prefab in 'Resources' folder<br/>
Use [PhotonNetwork.Instantiate](http://doc-api.photonengine.com/en/pun/current/class_photon_network.html#a843d9f62d28ab123c83291c1e6bb857d) to instantiate object<br/>
<br/>
You should add Photon View component to sync data<br/>
You can use Photon Transform View, Photon Animator View and some others Photon View component for sync common things, just add view to Photon View list<br/>
To sync uncommon data use class that implements [IPunObservable](http://doc-api.photonengine.com/en/pun/current/interface_i_pun_observable.html) interface<br/>
Don't forget to add script to object to sync and add script in Photon View list<br/>
<br/>
You can use [stream.isWriting](http://doc-api.photonengine.com/en/pun/current/class_photon_stream.html#a99e20ecd7737381042751acf8ec8fc4b) to find out, is it your object.<br/>
Extend from [Photon.PunBehaviour](http://doc-api.photonengine.com/en/pun/current/class_photon_1_1_pun_behaviour.html) instead of MonoBehaviour in this classes to access Photon fields and methods<br/>
Use [Photon documentation](http://doc-api.photonengine.com/en/pun/current/classes.html) or just contact me)<br/>
