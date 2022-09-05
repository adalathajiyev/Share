.DEFAULT=help

red        := $(shell tput setaf 1)
green      := $(shell tput setaf 2)
reset      := $(shell tput sgr0)
bold       := $(shell tput bold)
underline  := $(shell tput smul)

help : Makefile
	@sed -n 's/^##//p' $<

##
## -------------- postgres
## run                        : Runs docker-compose up for postgres and pgadmin
run:
	docker-compose up -d

## stop                       : Stops postgres and pgadmin services
stop:
	docker-compose stop 

## clean                      : Completely reset postgres and pgadmin
clean:
	docker-compose down -v

controlled-start: run
##


