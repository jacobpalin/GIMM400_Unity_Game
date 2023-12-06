#define Xaxis_pin A3 // Arduino pin connected to the VRx Pin
#define Yaxis_pin A2 // Arduino pin connected to the VRy Pin
#define SW_pin A1 // Arduino pin connected to the SW Pin

//#define buttonPin A0

void setup() {
  // put your setup code here, to run once:
  pinMode(SW_pin, INPUT);
  digitalWrite(SW_pin, HIGH);
  //pinMode(buttonPin, INPUT);
//  digitalWrite(buttonPin, HIGH);
  Serial.begin(9600);
}

void loop() {
  // put your main code here, to run repeatedly:
  //Serial.print("Button: ");
  //Serial.print(digitalRead(buttonPin));
  //Serial.print(" : ");
  Serial.print("X-axis: ");
  Serial.print(analogRead(Xaxis_pin));
  Serial.print(" : ");
  Serial.print("Y-axis: ");
  Serial.print(analogRead(Yaxis_pin));
  Serial.print(" : ");
  Serial.print("Switch:  ");
  Serial.println(digitalRead(SW_pin));
  delay(200);
}
