.PHONY: infra-local

infra-local:
 @docker-compose up -d db db
 @docker-compose up --abort-on-container-exit --exit-code-from app app