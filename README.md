# Task-5

Video Rental Service
Some kind of video rental store. 

Delevoped with .Net Framework, ASP.NET MVC, Entity Framework, Razor.
Included a few Unit Tests for Business Layer.
Was applied N-Layer architecture. AutoMapper for mapping objects from different Layers. Repository pattern for encapsulating DAL. And The Unit of Work pattern which makes it easy to work with different repositories and gives confidence that all repositories will use the same Dbcontext.

For Authentication and Authorization were used ASP.Net Identity. Also was implemented the Facebook Login feature. Depending on the role, your rights as a service user can both expand and be reduced.

For Logging Unhandled Exceptions in DB were applied Elmah.
For application optimization, real-time diagnostic, insights - Glimpse.

To run app you need to setup IIS or to use IISExpress.
Used code-first approach, this way database will be created automatically.  
