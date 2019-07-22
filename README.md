# LookUpValue
Dinamik konfigure verilerin saklanması;<br/><br/>
Kullanılan Teknolojiler;<br/><br/>
•	Net Core <br/>
•	Redis Cache <br/>
•	PostgreSQL <br/>
•	Mail Manager (SMTP Provider) <br/>
•	Log Manager (NLog) <br/>
•	Exception Middleware <br/>
•	JWT Token <br/><br/>
Uygulama geliştirme platform olarak Net Core seçildi. SOLID prensiplerine bağlı kalarak geliştirilen uygulamada, Net Core tercih sebebi windows platformuna bağlı kalmadan uygulamları host edebilmek ve ölçeklenebilir uygulamlar geliştirmek için seçilmiştir.<br/><br/>
Data, PostgreSQL üzerinde saklandı. NoSQL bir çözüm olmasa da sağladığı transaction sayılarında yüksek performans göstermesi ve open source bir uygulama olması tercih sebebi.<br/><br/>
Cache uygulaması olarak bir “Cache Manager” yazıldı ve “Redis” kullanıldı. Başarılı bir şekilde görevini yerine getirmesi döküman sayısının fazla olması ve hali hazırda kullanan referans firmalar Redis seçiminde önemli rol oynadı.<br/><br/>
Uygulama için bir “Log Manager” yazıldı ve NLog kullanıldı. Log dataları PostgreSQL üzerinde saklandı. Yazılan bir “Exception Middleware” ile hata logları otomatik olarak yapıldı. Diğer info vb log dataları yazılan “LogManager” ile geliştirici insiyatifine bırakıldı.<br/><br/>
Role yapısı ve authentication için “JWT” kullanıldı.<br/><br/>
Projeye “Mail Manager” yazıldı ve SMTP Provider entegre edildi. Kurgulanan yapıda isternirse farklı provider eklenebilir,  geliştirme bu şekilde yapıldı.<br/><br/>

Osman KURT
info@osmankurt.net

