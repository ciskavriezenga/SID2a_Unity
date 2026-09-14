

- om te kijken op welke branch je zit:
`git branch`

- om te kijken op welke branches er allemaal bestaan, ook online:
`git branch -a`

- update lokaal t.o.v. de online repository eerst met
`git fetch -p`

- om over te gaan op een andere branch (hieronder 'branchname' genoemd), bijv. een nieuwe die online staat
`git checkout branchname`
- bijv. de branch session3:
`git checkout session3`
  Krijg je dan een melding dat er bestanden zijn gewijzigd en wil je de wijzigingen even 'opzij zetten':
  `git stash`
  Krijg je een melding dat er untracked files worden overschreven?
  `git checkout session3 -f`
