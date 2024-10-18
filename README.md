Las entidades principales que desarrollamos para el proyecto son employees y requests, 

estas ultimas son una entidad comun que incluye infrmacion de solicitude sy su estado 

("denegadas", "aceptadas", "en espera") , employees es una entidad que ereda de 

La IdentityUser de siempre pero agregandole propiedades que consideramos para 

emlpleados como puesto y fecha de inicio de trabajo,. en servicios tenemos un servicio 

comun de crud para empleados con la diferencia de  usa usermanager en lugar del contexto 

para metodos relacionados a crud, hicimos la funcionalidad para servicio de slicitudes, 

con enpoint para ver por id, que muestra solo solicitudes que se asemejen al id que obtuvo

del token, y un get all, utilizamos la entidad employeeEntity en lugar de identityuser en 

todos los espacios disponibles donde se requeria, hicimos las tablas  a travez del contexto 

y configuramos a requests en su propio configure, el controlador de employees era para el 

crud y solo se le dio acceso a tokens de rol sdmin o hr, y en el controlador de requests 

le dimos autoridad a hr de ver todos los request y a todos los que tengan un token el servicio 

de crear una solicitud, tambien la funcion de ver tus propios solicitudes, no tenia 

autentificacion porque obtenia el id del token de todas formas