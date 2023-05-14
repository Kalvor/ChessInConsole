Main goal and requirements
1. Create game off chess, which will allow to perform valid chess match, with all allowed moves and all possible ends of game
2. Connect two player having exe file on their pc via lan and perform match
3. Allow player to perform game locally on their machine
4. Establish stable connection between both hosts 
5. Define in game clock which can be set 
6. Handle player connection via list of all avaliable players and invites (some sort of queue set)


Solution architecture
In order to realize all nescessary requirements, there is need to create 
- UI project containing all layer of presentation and input gathering
- Networking/Connection project containing all logic related to connection, match making and data transmision 
- Chess project which will handle and validate all inserted moves and resolve state of game (checkmate, stalemate etc.)
