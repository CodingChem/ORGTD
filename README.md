# ORGTD
**WIP**

The plan is to make a mobile app which is a combo of notes, tasks and calendar app. 
The Repository should be able to parse and be initialized from org files.
As this is mostly beeing developed for my use case the organization of roam files wil mirror my current setup and hopefully be configurable in the future.

## Org dir structure


>[!NOTE] 
Subject to change as i refine my setup

```
.
├── archive
├── gtd
│  ├── projects.org
│  ├── someday.org
│  └── todo.org
└── roam
   └── daily
      └── YYYY-MM-DD.org
```

## TODO
- [ ] Notes Page
- [ ] Daily Journal Page
- [ ] Tasks Page
- [ ] Agenda Page
- [ ] Org Parser
  - [ ] Journal Repository
  - [ ] Tasks Repository
  - [ ] Notes Repository?
        I am fond of [Org-Roam](https://www.orgroam.com/) I would need to think about how to implement this.
  - [ ] Events reopository?
    - [ ] Essentially an event would be either a note or a task with a scheduled date or a deadline. or maybe just elements with an event tag? 
