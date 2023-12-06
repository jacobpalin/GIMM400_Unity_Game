#define Xaxis_pin A3 // Arduino pin connected to the VRx Pin
#define Yaxis_pin A2 // Arduino pin connected to the VRy Pin
#define SW_pin A1 // Arduino pin connected to the SW Pin

void setup() {
  // put your setup code here, to run once:
  pinMode(SW_pin, INPUT);
  digitalWrite(SW_pin, HIGH);
  Serial.begin(57600);
}

void loop() {
  Serial.print(" ");
  Serial.print(analogRead(Xaxis_pin));
  Serial.print(" ");
  Serial.print(analogRead(Yaxis_pin));
  Serial.print(" ");
  Serial.println(digitalRead(SW_pin));
  delay(200);
}
