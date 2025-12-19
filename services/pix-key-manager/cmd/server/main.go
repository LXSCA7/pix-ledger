package main

import (
	"fmt"
	"log"
	"net"

	"github.com/LXSCA7/pix-ledger/services/pix-key-manager/internal/services"
	pb "github.com/LXSCA7/pix-ledger/services/pix-key-manager/pb/proto"
	"google.golang.org/grpc"
)

func main() {
	port := ":50051"
	listener, err := net.Listen("tcp", port)
	if err != nil {
		log.Fatalf("error opening server at %s: %v", port, err)
	}

	grpcServer := grpc.NewServer()
	pixService := services.NewPixGrpcService()
	pb.RegisterPixServiceServer(grpcServer, pixService)

	fmt.Printf("🤓 grpc running at %s\n", port)
	if err := grpcServer.Serve(listener); err != nil {
		log.Fatalf("%v", err)
	}
}
