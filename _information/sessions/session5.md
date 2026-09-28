# Session 5


## Opdracht - jump sounds

Voeg geluid toe voor de start én stop van een jump
Stappenplan:

1. Klap in de hierarchy de player uit en voeg onder player 2x een audiosource object toe:
  - jumpStartAudioSource
  - jumpStopAudioSource
  _(Eventueel kun je de audiosource bij de voeten van de player in de wereld positioneren)_

2. Open onderin in het project tabje de assets/audio folder _(voeg eventueel zelf twee sounds toe aan de folder )_
Selecteer in de hierachy de desbetreffende audiosource en sleep audiofile in de inspector naar de audiosource 'audioGenerator'.

3. Voeg nu de nodige afspeel code aan het characterAudio script. D.w.z., voeg 2x, voor zowel de start als de end, het volgende toe:
  - functie
  - member (= class variable) AudioSource

4. Roep vanuit het PlayerController script op de juiste plek de twee jump start en stop afspeel functies aan.
