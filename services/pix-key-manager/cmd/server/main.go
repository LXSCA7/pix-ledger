package main

import (
	"fmt"
	"log"
	"net"
	"os"

	"github.com/LXSCA7/pix-ledger/services/pix-key-manager/internal/services"
	pb "github.com/LXSCA7/pix-ledger/services/pix-key-manager/pb/proto"
	"google.golang.org/grpc"
)

func main() {
	port := os.Getenv("GRPC_PORT")
	if port == "" {
		port = "50051"
	}

	listener, err := net.Listen("tcp", fmt.Sprintf(":%s", port))
	if err != nil {
		log.Fatalf("error opening server at %s: %v", port, err)
	}

	addr := os.Getenv("REDIS_ADDRESS")
	if addr == "" {
		addr = "localhost:6379"
	}

	grpcServer := grpc.NewServer()
	pixService := services.NewPixGrpcService(addr)
	pb.RegisterPixServiceServer(grpcServer, pixService)

	fmt.Printf("🤓 grpc running at %s\n", port)
	if err := grpcServer.Serve(listener); err != nil {
		log.Fatalf("%v", err)
	}
}
