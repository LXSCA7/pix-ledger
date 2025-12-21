.PHONY: all proto run-go run-dotnet up down logs

PROTO_DIR = proto/pix.proto
GO_DIR = services/pix-key-manager
DOTNET_DIR = src/PixLedger.Api

proto:
	@echo "generating protobuf files"
	@echo "	[1/2] generating golang"
	@rm -f $(GO_DIR)/pb/*.go
	protoc -I=proto \
		--go_out=$(GO_DIR)/pb/proto --go_opt=paths=source_relative \
		--go-grpc_out=$(GO_DIR)/pb/proto --go-grpc_opt=paths=source_relative \
		pix.proto
	@echo "	[2/2] generating dotnet"
	dotnet build $(DOTNET_DIR) --nologo --verbosity quiet

run-go:
	@echo "running go grpc..."
	cd $(GO_DIR) && go run cmd/server/main.go

watch-run-dotnet:
	@echo "running .net api..."
	dotnet watch run --project $(DOTNET_DIR)

run-dotnet:
	@echo "running .net api..."
	dotnet run --project $(DOTNET_DIR)

up:
	docker-compose up -d

up-all:
	docker-compose --profile app up -d

down:
	docker compose --profile app down

tree:
	tree -I "bin|obj|Properties|*.user|*.cache"

k6:
	@k6 run tests/load-test.js    