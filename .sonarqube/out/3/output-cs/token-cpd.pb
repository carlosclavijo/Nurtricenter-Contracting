®
aC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Program.cs
var 
builder 
= 
WebApplication 
. 
CreateBuilder *
(* +
args+ /
)/ 0
;0 1
string		 
serviceName		 
=		 
$str		 &
;		& '
builder 
. 
Host 
. 

UseLogging 
( 
serviceName #
,# $
builder% ,
., -
Configuration- :
): ;
;; <
builder 
. 
Services 
. 
AddApplication 
( 
) 
. 
AddInfrastructure 
( 
builder 
. 
Configuration )
,) *
builder+ 2
.2 3
Environment3 >
,> ?
serviceName@ K
)K L
. 
AddPresentation 
( 
builder 
. 
Configuration '
,' (
builder) 0
.0 1
Environment1 <
)< =
;= >
var 
app 
= 	
builder
 
. 
Build 
( 
) 
; 
if 
( 
app 
. 
Environment 
. 
IsDevelopment !
(! "
)" #
)# $
{ 
app 
. 
UseSwaggerWithUi 
( 
) 
; 
app 
. 
ApplyMigrations 
( 
) 
; 
} 
app 
. 

UseRouting 
( 
) 
; 
app 
. 
UseHealthChecks 
( 
) 
; 
app!! 
.!! #
UseRequestCorrelationId!! 
(!! 
)!! 
;!! 
app## 
.## $
UseRequestContextLogging## 
(## 
)## 
;## 
app%% 
.%% 
UseExceptionHandler%% 
(%% 
)%% 
;%% 
app'' 
.'' 
UseHttpsRedirection'' 
('' 
)'' 
;'' 
app)) 
.)) 
UseAuthorization)) 
()) 
))) 
;)) 
app++ 
.++ 
MapControllers++ 
(++ 
)++ 
;++ 
app-- 
.-- 
Run-- 
(-- 
)-- 	
;--	 
∆
vC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Middleware\TracingMiddleware.cs
	namespace 	
Catalog
 
. 
WebApi 
. 

Middleware #
;# $
public 
class 
TracingMiddleware 
( 
RequestDelegate .
Next/ 3
)3 4
{ 
private 
const 
string  
_correlationIdHeader -
=. /
$str0 B
;B C
public		 

async		 
Task		 
InvokeAsync		 !
(		! "
HttpContext		" -
context		. 5
,		5 6
ITracingProvider		7 G
tracingProvider		H W
)		W X
{

 
var 
activity 
= 
Activity 
.  
Current  '
;' (
if 

(
 
activity 
!= 
null 
) 
{ 	
tracingProvider 
. 

SetTraceId &
(& '
activity' /
./ 0
TraceId0 7
.7 8
ToString8 @
(@ A
)A B
)B C
;C D
tracingProvider 
. 
	SetSpanId %
(% &
activity& .
.. /
SpanId/ 5
.5 6
ToString6 >
(> ?
)? @
)@ A
;A B
} 	
var  
requestCorrelationId  
=! "
GetCorrelationId# 3
(3 4
context4 ;
,; <
tracingProvider= L
)L M
;M N,
 AddCorrelationIdHeaderToResponse (
(( )
context) 0
,0 1 
requestCorrelationId2 F
)F G
;G H
await 
Next 
( 
context 
) 
; 
} 
private 
string 
GetCorrelationId #
(# $
HttpContext$ /
context0 7
,7 8
ITracingProvider9 I
tracingProviderJ Y
)Y Z
{ 
if 

( 
context 
. 
Request 
. 
Headers #
.# $
TryGetValue$ /
(/ 0 
_correlationIdHeader0 D
,D E
outF I
var !
existingCorrelationId %
)% &
)& '
{ 	
tracingProvider   
.   
SetCorrelationId   ,
(  , -!
existingCorrelationId  - B
)  B C
;  C D
return!! !
existingCorrelationId!! (
;!!( )
}"" 	
return## 
tracingProvider## 
.## 
GetCorrelationId## /
(##/ 0
)##0 1
;##1 2
}$$ 
private&& 
static&& 
void&& ,
 AddCorrelationIdHeaderToResponse&& 8
(&&8 9
HttpContext&&9 D
context&&E L
,&&L M
string&&N T
correlationId&&U b
)&&b c
{'' 
context(( 
.(( 
Response(( 
.(( 

OnStarting(( #
(((# $
((($ %
)((% &
=>((' )
{((* +
context)) 
.)) 
Response)) 
.)) 
Headers)) $
.))$ %
Add))% (
())( ) 
_correlationIdHeader))) =
,))= >
new))? B
[))B C
]))C D
{))E F
correlationId** 
}++ 
)++ 
;++ 	
return,, 
Task,, 
.,, 
CompletedTask,, %
;,,% &
}-- 	
)--	 

;--
 
}.. 
}// ˇ
ÑC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Middleware\RequestContextLoggingMiddleware.cs
	namespace 	
Catalog
 
. 
WebApi 
. 

Middleware #
;# $
public 
class +
RequestContextLoggingMiddleware ,
(, -
RequestDelegate- <
next= A
)A B
{ 
public		 

Task		 
Invoke		 
(		 
HttpContext		 "
context		# *
,		* +
ITracingProvider		, <!
correlationIdProvider		= R
)		R S
{

 
using 
( 

LogContext 
. 
PushProperty &
(& '
$str' 6
,6 7!
correlationIdProvider8 M
.M N
GetCorrelationIdN ^
(^ _
)_ `
)` a
)a b
{ 	
return 
next 
. 
Invoke 
( 
context &
)& '
;' (
} 	
} 
} ë%
|C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Infrastructure\ServiceRegistration.cs
	namespace 	
Contracting
 
. 
WebApi 
. 
Infrastructure +
;+ ,
public 
class 
ServiceRegistration  
(  !
ConsulClient! -
consulClient. :
,: ;
IConfiguration< J
configurationK X
,X Y
ILoggerZ a
<a b
ServiceRegistrationb u
>u v
loggerw }
)} ~
:	 Ä
IHostedService
Å è
{ 
private 
string	 
_registrationId 
=  !
string" (
.( )
Empty) .
;. /
public		 
async		 
Task		 

StartAsync		 
(		 
CancellationToken		 /
cancellationToken		0 A
)		A B
{

 
string 
? 	
serviceAddress
 
= 
configuration (
.( )
GetValue) 1
<1 2
string2 8
?8 9
>9 :
(: ;
$str; U
)U V
;V W
int 
? 
servicePort 
= 
configuration "
." #
GetValue# +
<+ ,
int, /
?/ 0
>0 1
(1 2
$str2 L
)L M
;M N
string 
? 	
serviceName
 
= 
configuration %
.% &
GetValue& .
<. /
string/ 5
?5 6
>6 7
(7 8
$str8 W
)W X
;X Y
var 
registration 
= 
new $
AgentServiceRegistration 1
{ 
ID 
= 
$" 

{
 
serviceName 
} 
$str 
{ 
Guid 
. 
NewGuid %
(% &
)& '
}' (
"( )
,) *
Name 
= 	
serviceName
 
, 
Address 

= 
serviceAddress 
, 
Port 
= 	
servicePort
 
== 
null 
? 
$num  !
:" #
servicePort$ /
./ 0
Value0 5
,5 6
Check 
=	 

new 
AgentServiceCheck  
{ 
HTTP 
=	 

$" 
{ 
serviceAddress 
} 
$str 
{ 
servicePort *
}* +
$str+ 2
"2 3
,3 4
Interval 
= 
TimeSpan 
. 
FromSeconds #
(# $
$num$ &
)& '
,' (*
DeregisterCriticalServiceAfter "
=# $
TimeSpan% -
.- .
FromSeconds. 9
(9 :
$num: <
)< =
,= >
Method 

= 
$str 
} 
} 
; 
_registrationId 
= 
registration  
.  !
ID! #
;# $
logger 
. 	
LogInformation	 
( 
$str ;
); <
;< =
await 
consulClient 
. 
Agent 
. 
ServiceRegister *
(* +
registration+ 7
,7 8
cancellationToken9 J
)J K
;K L
}!! 
public## 
async## 
Task## 
	StopAsync## 
(## 
CancellationToken## .
cancellationToken##/ @
)##@ A
{$$ 
logger%% 
.%% 	
LogInformation%%	 
(%% 
$str%% >
)%%> ?
;%%? @
if&& 
(&& 
!&& 
string&& 
.&& 
IsNullOrEmpty&& 
(&& 
_registrationId&& +
)&&+ ,
)&&, -
{'' 
await(( 
consulClient((	 
.(( 
Agent(( 
.(( 
ServiceDeregister(( -
(((- .
_registrationId((. =
,((= >
cancellationToken((? P
)((P Q
;((Q R
})) 
logger** 
.** 	
LogInformation**	 
(** 
$str** 1
)**1 2
;**2 3
}++ 
},, È$
wC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Infrastructure\ResponseHelper.cs
	namespace 	
Contracting
 
. 
WebApi 
. 
Infrastructure +
;+ ,
public 
class 
ResponseHelper 
{ 
public 
static 
string 
GetTitle 
( 
Error $
error% *
)* +
=>, .
error		 	
.			 

Type		
 
switch		 
{

 
	ErrorType 
. 

Validation 
=> 
error "
." #
Code# '
,' (
	ErrorType 
. 
Problem 
=> 
error 
.  
Code  $
,$ %
	ErrorType 
. 
NotFound 
=> 
error  
.  !
Code! %
,% &
	ErrorType 
. 
Conflict 
=> 
error  
.  !
Code! %
,% &
_ 
=> 	
$str
 
} 
; 
public 
static 
string 
	GetDetail 
(  
Error  %
error& +
)+ ,
=>- /
error 
. 
Type 
switch 
{ 
	ErrorType 
. 

Validation 
=> 
error  
.  !
Description! ,
,, -
	ErrorType 
. 
Problem 
=> 
error 
. 
Description )
,) *
	ErrorType 
. 
NotFound 
=> 
error 
. 
Description *
,* +
	ErrorType 
. 
Conflict 
=> 
error 
. 
Description *
,* +
_ 
=> 
$str &
} 
; 
public 
static 
string 
GetType 
( 
	ErrorType '
	errorType( 1
)1 2
=>3 5
	errorType 
switch 
{ 
	ErrorType 
. 

Validation 
=> 
$str N
,N O
	ErrorType   
.   
Problem   
=>   
$str   K
,  K L
	ErrorType!! 
.!! 
NotFound!! 
=>!! 
$str!! L
,!!L M
	ErrorType"" 
."" 
Conflict"" 
=>"" 
$str"" L
,""L M
_## 
=>## 
$str## ;
}$$ 
;$$ 
public&& 
static&& 
int&& 
GetStatusCode&&  
(&&  !
	ErrorType&&! *
	errorType&&+ 4
)&&4 5
=>&&6 8
	errorType'' 
switch'' 
{(( 
	ErrorType)) 
.)) 

Validation)) 
=>)) 
StatusCodes)) &
.))& '
Status400BadRequest))' :
,)): ;
	ErrorType** 
.** 
NotFound** 
=>** 
StatusCodes** $
.**$ %
Status404NotFound**% 6
,**6 7
	ErrorType++ 
.++ 
Conflict++ 
=>++ 
StatusCodes++ $
.++$ %
Status409Conflict++% 6
,++6 7
_,, 
=>,, 
StatusCodes,, 
.,, (
Status500InternalServerError,, 0
}-- 
;-- 
public// 
static// 

Dictionary// 
<// 
string//  
,//  !
object//" (
?//( )
>//) *
?//* +
	GetErrors//, 5
(//5 6
Result//6 <
result//= C
)//C D
{00 
if11 
(11 
result11 
.11 
Error11 
is11 
not11 
ValidationError11 )
validationError11* 9
)119 :
{22 
return33 	
null33
 
;33 
}44 
return66 
new66	 

Dictionary66 
<66 
string66 
,66 
object66  &
?66& '
>66' (
{77 
{88 
$str88 
,88 
validationError88 
.88  
Errors88  &
}88' (
}99 
;99 
}:: 
};; ≤
C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Infrastructure\GlobalExceptionHAndler.cs
	namespace 	
Contracting
 
. 
WebApi 
. 
Infrastructure +
;+ ,
internal 
sealed	 
class "
GlobalExceptionHandler ,
(, -
ILogger- 4
<4 5"
GlobalExceptionHandler5 K
>K L
loggerM S
)S T
: 
IExceptionHandler 
{		 
public

 
async

 
	ValueTask

 
<

 
bool

 
>

 
TryHandleAsync

 ,
(

, -
HttpContext 
httpContext 
, 
	Exception 
	exception 
, 
CancellationToken 
cancellationToken %
)% &
{ 
logger 
. 	
LogError	 
( 
	exception 
, 
$str ;
); <
;< =
if 
( 
	exception 
is 
DomainException "
domainException# 2
)2 3
{ 
var 
error 
= 
domainException 
. 
Error $
;$ %
var  
domainProblemDetails 
= 
new !
ProblemDetails" 0
{ 
Status 

= 
ResponseHelper 
. 
GetStatusCode )
() *
error* /
./ 0
Type0 4
)4 5
,5 6
Title 	
=
 
ResponseHelper 
. 
GetTitle #
(# $
error$ )
)) *
,* +
Detail 

= 
ResponseHelper 
. 
	GetDetail %
(% &
error& +
)+ ,
,, -
Type 
=	 

ResponseHelper 
. 
GetType !
(! "
error" '
.' (
Type( ,
), -
,- .
} 
; 
httpContext 
. 
Response 
. 

StatusCode "
=# $ 
domainProblemDetails% 9
.9 :
Status: @
.@ A
ValueA F
;F G
await 
httpContext	 
. 
Response 
. 
WriteAsJsonAsync .
(. / 
domainProblemDetails/ C
,C D
cancellationTokenE V
)V W
;W X
return 	
true
 
; 
} 
var   
problemDetails   
=   
new   
ProblemDetails   )
{!! 
Status"" 	
=""
 
StatusCodes"" 
."" (
Status500InternalServerError"" 4
,""4 5
Type## 
=## 	
$str##
 G
,##G H
Title$$ 
=$$	 

$str$$ 
}%% 
;%% 
httpContext'' 
.'' 
Response'' 
.'' 

StatusCode'' !
=''" #
problemDetails''$ 2
.''2 3
Status''3 9
.''9 :
Value'': ?
;''? @
await)) 
httpContext)) 
.)) 
Response)) 
.)) 
WriteAsJsonAsync)) -
())- .
problemDetails)). <
,))< =
cancellationToken))> O
)))O P
;))P Q
return++ 
true++	 
;++ 
},, 
}-- Ø
yC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Infrastructure\CustomController.cs
	namespace 	
Contracting
 
. 
WebApi 
. 
Infrastructure +
;+ ,
public 
class 
CustomController 
: 
ControllerBase  .
{ 
public 
IActionResult 
BuildResult !
<! "
T" #
># $
($ %
Result% +
<+ ,
T, -
>- .
result/ 5
)5 6
=>7 9
result		 
.		 	
	IsSuccess			 
?

 
Ok

 
(

 
result

 
.

 
Value

 
)

 
: 
Problem 
( 
result 
) 
; 
private 
ObjectResult	 
Problem 
( 
Result $
result% +
)+ ,
{ 
if 
( 
result 
. 
	IsSuccess 
) 
{ 
throw 
new	 %
InvalidOperationException &
(& '
)' (
;( )
} 
int 

statusCode 
= 
ResponseHelper !
.! "
GetStatusCode" /
(/ 0
result0 6
.6 7
Error7 <
.< =
Type= A
)A B
;B C
var 
problemDetails 
= 
new 
ProblemDetails )
{ 
Status 	
=
 

statusCode 
, 
Title 
=	 

ResponseHelper 
. 
GetTitle "
(" #
result# )
.) *
Error* /
)/ 0
,0 1
Detail 	
=
 
ResponseHelper 
. 
	GetDetail $
($ %
result% +
.+ ,
Error, 1
)1 2
,2 3
Type 
= 	
ResponseHelper
 
. 
GetType  
(  !
result! '
.' (
Error( -
.- .
Type. 2
)2 3
,3 4
} 
; 
return 

StatusCode	 
( 

statusCode 
, 
problemDetails  .
). /
;/ 0
} 
} ¶	
xC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Extensions\MigrationExtensions.cs
	namespace 	
Contracting
 
. 
WebApi 
. 

Extensions '
;' (
public 
static 
class 
MigrationExtensions '
{ 
public 
static 
void 
ApplyMigrations #
(# $
this$ (
IApplicationBuilder) <
app= @
)@ A
{ 
using		 
IServiceScope		 
scope		 
=		 
app		 !
.		! "
ApplicationServices		" 5
.		5 6
CreateScope		6 A
(		A B
)		B C
;		C D
using 
var 
db 
= 
scope 
. 	
ServiceProvider	 
. 
GetRequiredService +
<+ ,
	IDatabase, 5
>5 6
(6 7
)7 8
;8 9
db 
. 
Migrate 
( 
) 
; 
} 
} î

yC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Extensions\MiddlewareExtensions.cs
	namespace 	
Contracting
 
. 
WebApi 
. 

Extensions '
;' (
public 
static 
class  
MiddlewareExtensions (
{ 
public 
static 
IApplicationBuilder "#
UseRequestCorrelationId# :
(: ;
this; ?
IApplicationBuilder@ S
appT W
)W X
{ 
app		 
.		 
UseMiddleware		 
<		 
TracingMiddleware		 %
>		% &
(		& '
)		' (
;		( )
return

 
app

	 
;

 
} 
public 
static 
IApplicationBuilder "$
UseRequestContextLogging# ;
(; <
this< @
IApplicationBuilderA T
appU X
)X Y
{ 
app 
. 
UseMiddleware 
< +
RequestContextLoggingMiddleware 3
>3 4
(4 5
)5 6
;6 7
return 
app	 
; 
} 
} ñ
ÅC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Extensions\ApplicationBuilderExtensions.cs
	namespace 	
Contracting
 
. 
WebApi 
. 

Extensions '
;' (
public 
static 
class (
ApplicationBuilderExtensions 0
{ 
public 
static 
IApplicationBuilder "
UseSwaggerWithUi# 3
(3 4
this4 8
WebApplication9 G
appH K
)K L
{		 
app

 
.

 

UseSwagger

 
(

 
)

 
;

 
app 
. 
UseSwaggerUI 
( 
) 
; 
return 
app	 
; 
} 
public 
static 
void 
UseHealthChecks #
(# $
this$ (
WebApplication) 7
app8 ;
); <
{ 
app 
. 
MapHealthChecks 
( 
$str 
,  
new! $
HealthCheckOptions% 7
{ 
ResponseWriter 
= 
UIResponseWriter $
.$ %&
WriteHealthCheckUIResponse% ?
} 
) 
; 
} 
} •
mC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\DependencyInjection.cs
	namespace 	
Contracting
 
. 
WebApi 
; 
public 
static 
class 
DependencyInjection '
{ 
public 
static 
IServiceCollection !
AddPresentation" 1
(1 2
this2 6
IServiceCollection7 I
servicesJ R
,R S
IConfigurationT b
configurationc p
,p q
IHostEnvironment	r Ç
environment
É é
)
é è
{		 
services

 

.


 #
AddEndpointsApiExplorer

 "
(

" #
)

# $
;

$ %
services 

.
 
AddSwaggerGen 
( 
) 
; 
services 

.
 
AddControllers 
( 
) 
; 
services 

.
 
AddExceptionHandler 
< "
GlobalExceptionHandler 5
>5 6
(6 7
)7 8
;8 9
services 

.
 
AddProblemDetails 
( 
) 
; 
if 
( 
! 
environment 
. 
IsDevelopment  
(  !
)! "
)" #
{ 
services 
. -
!RegisterServiceToServiceDiscovery -
(- .
configuration. ;
); <
;< =
} 
return 
services	 
; 
} 
private 
static	 
IServiceCollection "-
!RegisterServiceToServiceDiscovery# D
(D E
thisE I
IServiceCollectionJ \
services] e
,e f
IConfigurationg u
configuration	v É
)
É Ñ
{ 
string 
? 	#
serviceDiscoveryAddress
 !
=" #
configuration$ 1
.1 2
GetValue2 :
<: ;
string; A
?A B
>B C
(C D
$strD q
)q r
;r s
services 

.
 
AddSingleton 
( 
sp 
=> 
new !
ConsulClient" .
(. /
c/ 0
=>1 3
c4 5
.5 6
Address6 =
=> ?
new@ C
UriD G
(G H#
serviceDiscoveryAddressH _
)_ `
)` a
)a b
;b c
services 

.
 
AddHostedService 
< 
ServiceRegistration /
>/ 0
(0 1
)1 2
;2 3
return   
services  	 
;   
}!! 
}"" ß*
wC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Controllers\PatientController.cs
	namespace 	
Contracting
 
. 
WebApi 
. 
Controllers (
;( )
[

 
Route

 
(

 
$str

 
)

 
]

 
[ 
ApiController 
] 
public 
class 
PatientController 
( 
	IMediator (
Mediator) 1
)1 2
:3 4
CustomController5 E
{ 
[ 
HttpPost 
] 
public 

async 
Task 
< 
IActionResult #
># $
CreatePatient% 2
(2 3
[3 4
FromBody4 <
]< = 
CreatePatientCommand> R
commandS Z
)Z [
{ 
try 
{ 	
var 
id 
= 
await 
Mediator #
.# $
Send$ (
(( )
command) 0
)0 1
;1 2
var 
createdPatient 
= 
await 
Mediator &
.& '
Send' +
(+ ,
new, /
GetPatientByIdQuery0 C
(C D
idD F
.F G
ValueG L
)L M
)M N
;N O
var 
response 
= 
new 
{ 
Patient 
= 
createdPatient 
. 
Value "
," #
Message 
= 
$str ,
} 
; 
return 	
Ok
 
( 
response 
) 
; 
} 
catch 
( 
	Exception 
ex 
) 
{ 	
return 

StatusCode 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
,F G
exH J
.J K
MessageK R
)R S
;S T
} 	
}   
["" 
HttpGet"" 
]"" 
public## 

async## 
Task## 
<## 
IActionResult## #
>### $
GetPatients##% 0
(##0 1
)##1 2
{$$ 
try%% 
{&& 	
var'' 
result'' 
='' 
await'' 
Mediator'' '
.''' (
Send''( ,
('', -
new''- 0
GetPatientsQuery''1 A
(''A B
$str''B D
)''D E
)''E F
;''F G
var(( 
response(( 
=(( 
new(( 
{)) 
Total** 
=** 
result** 
.** 
Count** $
(**$ %
)**% &
,**& '
Patients++ 
=++ 
result++ !
},, 
;,, 
return-- 
Ok-- 
(-- 
response-- 
)-- 
;--  
}.. 	
catch// 
(// 
	Exception// 
ex// 
)// 
{00 	
return11 

StatusCode11 
(11 
StatusCodes11 )
.11) *(
Status500InternalServerError11* F
,11F G
ex11H J
.11J K
Message11K R
)11R S
;11S T
}22 	
}33 
[55 
HttpGet55 
]55 
[66 
Route66 

(66
 
$str66 
)66 
]66 
public77 

async77 
Task77 
<77 
IActionResult77 #
>77# $
GetPatientById77% 3
(773 4
[774 5
	FromRoute775 >
]77> ?
Guid77@ D
id77E G
)77G H
{88 
try99 
{:: 	
var;; 
result;; 
=;; 
await;; 
Mediator;; '
.;;' (
Send;;( ,
(;;, -
new;;- 0
GetPatientByIdQuery;;1 D
(;;D E
id;;E G
);;G H
);;H I
;;;I J
if<< 
(<< 
result<< 
==<< 
null<< 
)<< 
{== 
var>> 
res>> 
=>> 
new>> 
{?? 
message@@ 
=@@ 
$str@@ 1
}AA 
;AA 
returnBB 
NotFoundBB 
(BB  
resBB  #
)BB# $
;BB$ %
}CC 
varDD 
responseDD 
=DD 
newDD 
{EE 
PatientFF 
=FF 
resultFF  
.FF  !
ValueFF! &
,FF& '
MessageGG 
=GG 
$strGG B
}HH 
;HH 
returnJJ 
OkJJ 
(JJ 
responseJJ 
)JJ 
;JJ  
}KK 	
catchLL 
(LL 
	ExceptionLL 
exLL 
)LL 
{MM 	
returnNN 

StatusCodeNN 
(NN 
$numNN !
,NN! "
exNN# %
.NN% &
MessageNN& -
)NN- .
;NN. /
}OO 	
}PP 
}QQ °à
xC:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Controllers\ContractController.cs
	namespace 	
Contracting
 
. 
WebApi 
. 
Controllers (
;( )
[ 
Route 
( 
$str 
) 
] 
[ 
ApiController 
] 
public 
class 
ContractController 
(  
	IMediator  )
Mediator* 2
)2 3
:4 5
CustomController6 F
{ 
[ 
HttpPost 

]
 
public 

async 
Task 
< 
IActionResult #
># $
CreateContract% 3
(3 4
[4 5
FromBody5 =
]= >!
CreateContractCommand? T
commandU \
)\ ]
{ 
try 
{ 	
var 
id 
= 
await 
Mediator #
.# $
Send$ (
(( )
command) 0
)0 1
;1 2
var 
createdContract 
=  !
await" '
Mediator( 0
.0 1
Send1 5
(5 6
new6 9 
GetContractByIdQuery: N
(N O
idO Q
.Q R
ValueR W
)W X
)X Y
;Y Z
var 
response 
= 
new 
{ 
Contract 
= 
createdContract 
. 
Value $
,$ %
Message 
= 
$str -
} 
; 
return   
Ok   
(   
response   
)   
;    
}!! 	
catch"" 
("" 
	Exception"" 
ex"" 
)"" 
{## 	
return$$ 

StatusCode$$ 
($$ 
$num$$ !
,$$! "
ex$$# %
.$$% &
Message$$& -
)$$- .
;$$. /
}%% 	
}&& 
[(( 
HttpGet(( 
](( 
public)) 

async)) 
Task)) 
<)) 
ActionResult)) "
>))" #
GetContracts))$ 0
())0 1
)))1 2
{** 
try++ 
{,, 	
var-- 
result-- 
=-- 
await-- 
Mediator-- '
.--' (
Send--( ,
(--, -
new--- 0
GetContractsQuery--1 B
(--B C
$str--C E
)--E F
)--F G
;--G H
var.. 
response.. 
=.. 
new.. 
{// 
Total00 
=00 
result00 
.00 
Count00 $
(00$ %
)00% &
,00& '
	Contracts11 
=11 
result11 "
}22 
;22 
return33 
Ok33 
(33 
response33 
)33 
;33  
}44 	
catch55 
(55 	
	Exception55	 
ex55 
)55 
{66 
return77 	

StatusCode77
 
(77 
StatusCodes77  
.77  !(
Status500InternalServerError77! =
,77= >
ex77? A
.77A B
Message77B I
)77I J
;77J K
}88 
}99 
[;; 
HttpGet;; 	
];;	 

[<< 
Route<< 
(<< 
$str<< 
)<< 
]<< 
public== 
async== 
Task== 
<== 
IActionResult==  
>==  !
GetContractById==" 1
(==1 2
[==2 3
	FromRoute==3 <
]==< =
Guid==> B
id==C E
)==E F
{>> 
try?? 
{@@ 
varAA 
resultAA 
=AA 
awaitAA 
MediatorAA 
.AA 
SendAA #
(AA# $
newAA$ ' 
GetContractByIdQueryAA( <
(AA< =
idAA= ?
)AA? @
)AA@ A
;AAA B
ifBB 
(BB 
resultBB 
==BB 
nullBB 
)BB 
{CC 
varDD 
resDD 
=DD 
newDD 
{EE 
messageFF 
=FF 
$strFF #
}GG 
;GG 
returnHH 

NotFoundHH 
(HH 
resHH 
)HH 
;HH 
}II 
varJJ 
responseJJ 
=JJ 
newJJ 
{KK 
ContractLL 
=LL 
resultLL 
.LL 
ValueLL 
,LL 
MessageMM 
=MM 
$strMM 7
}NN 
;NN 
returnOO 	
OkOO
 
(OO 
responseOO 
)OO 
;OO 
}PP 
catchQQ 
(QQ 	
	ExceptionQQ	 
exQQ 
)QQ 
{RR 
returnSS 	

StatusCodeSS
 
(SS 
$numSS 
,SS 
exSS 
.SS 
MessageSS $
)SS$ %
;SS% &
}TT 
}UU 
[WW 
HttpPostWW 

]WW
 
[XX 
RouteXX 

(XX
 
$strXX 
)XX 
]XX 
publicYY 

asyncYY 
TaskYY 
<YY 
IActionResultYY #
>YY# $
InProgressContractYY% 7
(YY7 8
[YY8 9
FromBodyYY9 A
]YYA B%
InProgressContractCommandYYC \
commandYY] d
)YYd e
{ZZ 
try[[ 
{\\ 	
bool]] 
result]] 
=]] 
await]] 
Mediator]]  (
.]]( )
Send]]) -
(]]- .
command]]. 5
)]]5 6
;]]6 7
if^^ 
(^^ 
result^^ 
)^^ 
{__ 
var`` 
contract`` 
=`` 
await`` 
Mediator`` !
.``! "
Send``" &
(``& '
new``' * 
GetContractByIdQuery``+ ?
(``? @
command``@ G
.``G H

ContractId``H R
)``R S
)``S T
;``T U
varaa 
responseaa 
=aa 
newaa 
{bb 
Messagecc 
=cc 
$strcc ,
,cc, -
Contractdd 
=dd 
contractdd 
.dd 
Valuedd 
}ee 
;ee 
returnff 

Okff 
(ff 
responseff 
)ff 
;ff 
}gg 
elsehh 
{ii 
varjj 
responsejj 
=jj 
newjj 
{kk 
Messagell 
=ll 
$strll 3
}mm 
;mm 
returnnn 


BadRequestnn 
(nn 
responsenn 
)nn 
;nn  
}oo 
}pp 	
catchqq 
(qq 
	Exceptionqq 
exqq 
)qq 
{rr 	
returnss 

StatusCodess 
(ss 
$numss !
,ss! "
exss# %
.ss% &
Messagess& -
)ss- .
;ss. /
}tt 	
}uu 
[ww 
HttpPostww 
]ww 
[xx 
Routexx 

(xx
 
$strxx 
)xx 
]xx 
publicyy 

asyncyy 
Taskyy 
<yy 
IActionResultyy #
>yy# $
CompleteContractyy% 5
(yy5 6
[yy6 7
FromBodyyy7 ?
]yy? @#
CompleteContractCommandyyA X
commandyyY `
)yy` a
{zz 
try{{ 
{|| 	
bool}} 
result}} 
=}} 
await}} 
Mediator}}  (
.}}( )
Send}}) -
(}}- .
command}}. 5
)}}5 6
;}}6 7
if~~ 
(~~ 
result~~ 
)~~ 
{ 
var
ÄÄ 
contract
ÄÄ 
=
ÄÄ 
await
ÄÄ 
Mediator
ÄÄ !
.
ÄÄ! "
Send
ÄÄ" &
(
ÄÄ& '
new
ÄÄ' *"
GetContractByIdQuery
ÄÄ+ ?
(
ÄÄ? @
command
ÄÄ@ G
.
ÄÄG H

ContractId
ÄÄH R
)
ÄÄR S
)
ÄÄS T
;
ÄÄT U
var
ÅÅ 
response
ÅÅ 
=
ÅÅ 
new
ÅÅ 
{
ÇÇ 
Message
ÉÉ 
=
ÉÉ 
$str
ÉÉ *
,
ÉÉ* +
Contract
ÑÑ 
=
ÑÑ 
contract
ÑÑ 
.
ÑÑ 
Value
ÑÑ 
}
ÖÖ 
;
ÖÖ 
return
ÜÜ 

Ok
ÜÜ 
(
ÜÜ 
response
ÜÜ 
)
ÜÜ 
;
ÜÜ 
}
áá 
else
àà 
{
ââ 
var
ää 
response
ää 
=
ää 
new
ää 
{
ãã 
Message
åå 
=
åå 
$str
åå 0
}
çç 
;
çç 
return
éé 


BadRequest
éé 
(
éé 
response
éé 
)
éé 
;
éé  
}
èè 
}
êê 
catch
ëë 
(
ëë 
	Exception
ëë 
ex
ëë 
)
ëë 
{
íí 	
return
ìì 

StatusCode
ìì 
(
ìì 
$num
ìì !
,
ìì! "
ex
ìì# %
.
ìì% &
Message
ìì& -
)
ìì- .
;
ìì. /
}
îî 	
}
ïï 
[
óó 
HttpGet
óó 	
]
óó	 

[
òò 
Route
òò 
(
òò 
$str
òò 
)
òò 
]
òò 
public
ôô 
async
ôô 
Task
ôô 
<
ôô 
IActionResult
ôô  
>
ôô  !
GetDeliveryDay
ôô" 0
(
ôô0 1
[
ôô1 2
	FromRoute
ôô2 ;
]
ôô; <
Guid
ôô= A
id
ôôB D
)
ôôD E
{
öö 
try
õõ 
{
úú 
var
ùù 
result
ùù 
=
ùù 
await
ùù 
Mediator
ùù 
.
ùù 
Send
ùù #
(
ùù# $
new
ùù$ '!
GetDeliveryDayQuery
ùù( ;
(
ùù; <
id
ùù< >
)
ùù> ?
)
ùù? @
;
ùù@ A
if
ûû 
(
ûû 
result
ûû 
==
ûû 
null
ûû 
)
ûû 
{
üü 
var
†† 
res
†† 
=
†† 
new
†† 
{
°° 
message
¢¢ 
=
¢¢ 
$str
¢¢ #
}
££ 
;
££ 
return
§§ 

NotFound
§§ 
(
§§ 
res
§§ 
)
§§ 
;
§§ 
}
•• 
var
¶¶ 
response
¶¶ 
=
¶¶ 
new
¶¶ 
{
ßß 
Delivery
®® 
=
®® 
result
®® 
.
®® 
Value
®® 
,
®® 
Message
©© 
=
©© 
$str
©© 7
}
™™ 
;
™™ 
return
´´ 	
Ok
´´
 
(
´´ 
response
´´ 
)
´´ 
;
´´ 
}
¨¨ 
catch
≠≠ 
(
≠≠ 	
	Exception
≠≠	 
ex
≠≠ 
)
≠≠ 
{
ÆÆ 
return
ØØ 	

StatusCode
ØØ
 
(
ØØ 
$num
ØØ 
,
ØØ 
ex
ØØ 
.
ØØ 
Message
ØØ $
)
ØØ$ %
;
ØØ% &
}
∞∞ 
}
±± 
[
≥≥ 
	HttpPatch
≥≥ 
]
≥≥ 
[
¥¥ 
Route
¥¥ 
(
¥¥ 
$str
¥¥ 
)
¥¥ 
]
¥¥ 
public
µµ 
async
µµ 
Task
µµ 
<
µµ 
IActionResult
µµ  
>
µµ  ! 
UpdateDeliveryDays
µµ" 4
(
µµ4 5
[
µµ5 6
FromBody
µµ6 >
]
µµ> ?'
UpdateDeliveryDaysCommand
µµ@ Y
command
µµZ a
)
µµa b
{
∂∂ 
try
∑∑ 
{
∏∏ 
var
ππ 
id
ππ 	
=
ππ
 
await
ππ 
Mediator
ππ 
.
ππ 
Send
ππ 
(
ππ  
command
ππ  '
)
ππ' (
;
ππ( )
var
∫∫ 
createdContract
∫∫ 
=
∫∫ 
await
∫∫ 
Mediator
∫∫ '
.
∫∫' (
Send
∫∫( ,
(
∫∫, -
new
∫∫- 0"
GetContractByIdQuery
∫∫1 E
(
∫∫E F
id
∫∫F H
.
∫∫H I
Value
∫∫I N
)
∫∫N O
)
∫∫O P
;
∫∫P Q
var
ªª 
response
ªª 
=
ªª 
new
ªª 
{
ºº 
Contract
ΩΩ 
=
ΩΩ 
createdContract
ΩΩ 
.
ΩΩ 
Value
ΩΩ $
,
ΩΩ$ %
Message
ææ 
=
ææ 
$str
ææ .
}
øø 
;
øø 
return
¿¿ 	
Ok
¿¿
 
(
¿¿ 
response
¿¿ 
)
¿¿ 
;
¿¿ 
}
¡¡ 
catch
¬¬ 
(
¬¬ 	
	Exception
¬¬	 
ex
¬¬ 
)
¬¬ 
{
√√ 
return
ƒƒ 	

StatusCode
ƒƒ
 
(
ƒƒ 
$num
ƒƒ 
,
ƒƒ 
ex
ƒƒ 
.
ƒƒ 
Message
ƒƒ $
)
ƒƒ$ %
;
ƒƒ% &
}
≈≈ 
}
∆∆ 
[
»» 
	HttpPatch
»» 
]
»» 
[
…… 
Route
…… 
(
…… 
$str
…… 
)
…… 
]
…… 
public
   
async
   
Task
   
<
   
IActionResult
    
>
    !
UpdateDeliveryDay
  " 3
(
  3 4
[
  4 5
FromBody
  5 =
]
  = >)
UpdateDeliveyDayByIdCommand
  ? Z
command
  [ b
)
  b c
{
ÀÀ 
try
ÃÃ 
{
ÕÕ 
var
ŒŒ 
id
ŒŒ 	
=
ŒŒ
 
await
ŒŒ 
Mediator
ŒŒ 
.
ŒŒ 
Send
ŒŒ 
(
ŒŒ  
command
ŒŒ  '
)
ŒŒ' (
;
ŒŒ( )
var
œœ 
createdContract
œœ 
=
œœ 
await
œœ 
Mediator
œœ '
.
œœ' (
Send
œœ( ,
(
œœ, -
new
œœ- 0"
GetContractByIdQuery
œœ1 E
(
œœE F
id
œœF H
.
œœH I
Value
œœI N
)
œœN O
)
œœO P
;
œœP Q
var
–– 
response
–– 
=
–– 
new
–– 
{
—— 
Contract
““ 
=
““ 
createdContract
““ 
.
““ 
Value
““ $
,
““$ %
Message
”” 
=
”” 
$str
”” ,
}
‘‘ 
;
‘‘ 
return
’’ 	
Ok
’’
 
(
’’ 
response
’’ 
)
’’ 
;
’’ 
}
÷÷ 
catch
◊◊ 
(
◊◊ 	
	Exception
◊◊	 
ex
◊◊ 
)
◊◊ 
{
ÿÿ 
return
ŸŸ 	

StatusCode
ŸŸ
 
(
ŸŸ 
$num
ŸŸ 
,
ŸŸ 
ex
ŸŸ 
.
ŸŸ 
Message
ŸŸ $
)
ŸŸ$ %
;
ŸŸ% &
}
⁄⁄ 
}
€€ 
[
›› 

HttpDelete
›› 
]
›› 
[
ﬁﬁ 
Route
ﬁﬁ 
(
ﬁﬁ 
$str
ﬁﬁ 
)
ﬁﬁ 
]
ﬁﬁ 
public
ﬂﬂ 
async
ﬂﬂ 
Task
ﬂﬂ 
<
ﬂﬂ 
IActionResult
ﬂﬂ  
>
ﬂﬂ  !
DeleteDeliveryDay
ﬂﬂ" 3
(
ﬂﬂ3 4
[
ﬂﬂ4 5
FromBody
ﬂﬂ5 =
]
ﬂﬂ= >&
DeleteDeliveryDayCommand
ﬂﬂ? W
command
ﬂﬂX _
)
ﬂﬂ_ `
{
‡‡ 
try
·· 
{
‚‚ 
var
„„ 
id
„„ 	
=
„„
 
await
„„ 
Mediator
„„ 
.
„„ 
Send
„„ 
(
„„  
command
„„  '
)
„„' (
;
„„( )
var
‰‰ 
createdContract
‰‰ 
=
‰‰ 
await
‰‰ 
Mediator
‰‰ '
.
‰‰' (
Send
‰‰( ,
(
‰‰, -
new
‰‰- 0"
GetContractByIdQuery
‰‰1 E
(
‰‰E F
id
‰‰F H
.
‰‰H I
Value
‰‰I N
)
‰‰N O
)
‰‰O P
;
‰‰P Q
var
ÂÂ 
response
ÂÂ 
=
ÂÂ 
new
ÂÂ 
{
ÊÊ 
Contract
ÁÁ 
=
ÁÁ 
createdContract
ÁÁ 
.
ÁÁ 
Value
ÁÁ $
,
ÁÁ$ %
Message
ËË 
=
ËË 
$str
ËË 1
}
ÈÈ 
;
ÈÈ 
return
ÍÍ 	
Ok
ÍÍ
 
(
ÍÍ 
response
ÍÍ 
)
ÍÍ 
;
ÍÍ 
}
ÎÎ 
catch
ÏÏ 
(
ÏÏ 	
	Exception
ÏÏ	 
ex
ÏÏ 
)
ÏÏ 
{
ÌÌ 
return
ÓÓ 	

StatusCode
ÓÓ
 
(
ÓÓ 
$num
ÓÓ 
,
ÓÓ 
ex
ÓÓ 
.
ÓÓ 
Message
ÓÓ $
)
ÓÓ$ %
;
ÓÓ% &
}
ÔÔ 
}
 
}ÒÒ ˚*
}C:\Users\CARLOS\Desktop\Aprendizaje\CSharp\Nurtricenter-Contracting\Contracting.WebApi\Controllers\AdministratorController.cs
	namespace 	
Contracting
 
. 
WebApi 
. 
Controllers (
;( )
[

 
Route

 
(

 
$str

 
)

 
]

 
[ 
ApiController 
] 
public 
class #
AdministratorController $
($ %
	IMediator% .
Mediator/ 7
)7 8
:9 :
CustomController; K
{ 
[ 
HttpPost 
] 
public 

async 
Task 
< 
IActionResult #
># $
CreateAdministrator% 8
(8 9
[9 :
FromBody: B
]B C&
CreateAdministratorCommandD ^
command_ f
)f g
{ 
try 
{ 	
var 
id 
= 
await 
Mediator #
.# $
Send$ (
(( )
command) 0
)0 1
;1 2
var  
createdAdministrator $
=% &
await' ,
Mediator- 5
.5 6
Send6 :
(: ;
new; >%
GetAdministratorByIdQuery? X
(X Y
idY [
.[ \
Value\ a
)a b
)b c
;c d
var 
response 
= 
new 
{ 
Administrator 
=  
createdAdministrator  4
.4 5
Value5 :
,: ;
Message 
= 
$str >
} 
; 
return 
Ok 
( 
response 
) 
;  
} 	
catch 
( 
	Exception 
ex 
) 
{ 	
return 

StatusCode 
( 
StatusCodes )
.) *(
Status500InternalServerError* F
,F G
exH J
.J K
MessageK R
)R S
;S T
}   	
}!! 
[## 
HttpGet## 
]## 
public$$ 

async$$ 
Task$$ 
<$$ 
IActionResult$$ #
>$$# $
GetAdministrators$$% 6
($$6 7
)$$7 8
{%% 
try&& 
{'' 	
var(( 
result(( 
=(( 
await(( 
Mediator(( '
.((' (
Send((( ,
(((, -
new((- 0"
GetAdministratorsQuery((1 G
(((G H
$str((H J
)((J K
)((K L
;((L M
var)) 
response)) 
=)) 
new)) 
{** 
Total++ 
=++ 
result++ 
.++ 
Count++ $
(++$ %
)++% &
,++& '
Administrators,, 
=,,  
result,,! '
}-- 
;-- 
return.. 
Ok.. 
(.. 
response.. 
).. 
;..  
}// 	
catch00 
(00 
	Exception00 
ex00 
)00 
{11 	
return22 

StatusCode22 
(22 
StatusCodes22 )
.22) *(
Status500InternalServerError22* F
,22F G
ex22H J
.22J K
Message22K R
)22R S
;22S T
}33 	
}44 
[66 
HttpGet66 
]66 
[77 
Route77 

(77
 
$str77 
)77 
]77 
public88 

async88 
Task88 
<88 
IActionResult88 #
>88# $ 
GetAdministratorById88% 9
(889 :
[88: ;
	FromRoute88; D
]88D E
Guid88F J
id88K M
)88M N
{99 
try:: 
{;; 	
var<< 
result<< 
=<< 
await<< 
Mediator<< '
.<<' (
Send<<( ,
(<<, -
new<<- 0%
GetAdministratorByIdQuery<<1 J
(<<J K
id<<K M
)<<M N
)<<N O
;<<O P
if== 
(== 
result== 
==== 
null== 
)== 
{>> 
var?? 
res?? 
=?? 
new?? 
{@@ 
messageAA 
=AA 
$strAA 7
}BB 
;BB 
returnCC 
NotFoundCC 
(CC  
resCC  #
)CC# $
;CC$ %
}DD 
varEE 
responseEE 
=EE 
newEE 
{FF 
AdministratorGG 
=GG 
resultGG  &
.GG& '
ValueGG' ,
,GG, -
MessageHH 
=HH 
$strHH H
}II 
;II 
returnKK 
OkKK 
(KK 
responseKK 
)KK 
;KK  
}LL 	
catchMM 
(MM 
	ExceptionMM 
exMM 
)MM 
{NN 	
returnOO 

StatusCodeOO 
(OO 
$numOO !
,OO! "
exOO# %
.OO% &
MessageOO& -
)OO- .
;OO. /
}PP 	
}QQ 
}RR 