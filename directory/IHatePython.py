import turtle

t = turtle.Turtle()
t.color("yellow")
s = turtle.Screen()
s.setup(700,700)
s.bgpic('root_resize.png')

f = open("output.txt", "r", encoding = "utf-16");
t.penup()
for i in f :
    x = i[0 : i.index(",")]
    z = i[i.index(",") + 2 : len(i) -1]
    t.goto(float(x) * 5 + 180, float(z) * 5 -100)
    t.pendown()
f.close();
