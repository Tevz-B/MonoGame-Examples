- odstrani `this.` pri nekaterih klicih metod (`this.Game.Method` =>`Game.Method`)
- spremembe imenovanja - drug standard pri C# kot pri ObjC (večinoma avtomatsko z IDE)
- Poenotavi klice metod z overloadanimi operatorji (`Vector2.MultiplyBy(normal, impact / mass)` => `collisionNormal * (impact / mass)`
- 


- zakomentirane vrstine ostanejo v objc

Eventi so drugacni,

Load resource se naredi z MonoGame Content builder, kjer se doloci propertije (ali je SpriteFont, filter, ...)

objc lahko klices staticne metode na dinamicnem tipu in tudi constructor.
inherit je drugacen kot pri obj c : v objecive c lahko podedujes staticne metode in jih klices na dinamicnem tipu

V C# to ni mogoce, staticne metode se neda podedovat, prav tako se iz tipa lahko naredi instanco samo z ACtivator classom, ki pa ima precej overheada.

c# ima auto properties, ki so dober syntaksni sladkorček pri uporabi interfacov.


Razlike so tudi zato ker sem razvijal za pc ne za tablico, kar je večinoma vplivalo samo v fazo zajemnja inputa.
