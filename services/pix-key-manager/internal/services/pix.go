package services

import (
	"context"
	"fmt"

	pb "github.com/LXSCA7/pix-ledger/services/pix-key-manager/pb/proto"
)

type PixGrpcService struct {
	pb.UnimplementedPixServiceServer
}

func NewPixGrpcService() *PixGrpcService {
	return &PixGrpcService{}
}

func (s *PixGrpcService) FindKey(ctx context.Context, req *pb.PixRequest) (*pb.PixKeyResponse, error) {
	fmt.Printf("recebi req, chave", req.Key)

	if req.Key == "123" {
		return &pb.PixKeyResponse{
			Exists: true,
			Active: true,
			UserId: "user123",
			Key:    "123",
		}, nil
	}

	return &pb.PixKeyResponse{Exists: false}, nil
}
