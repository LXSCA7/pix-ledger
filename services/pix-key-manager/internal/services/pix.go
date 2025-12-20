package services

import (
	"context"
	"fmt"

	pb "github.com/LXSCA7/pix-ledger/services/pix-key-manager/pb/proto"
	"github.com/google/uuid"
)

type PixGrpcService struct {
	pb.UnimplementedPixServiceServer
	keys map[string]string
}

func NewPixGrpcService() *PixGrpcService {
	return &PixGrpcService{
		keys: make(map[string]string),
	}
}

func (s *PixGrpcService) FindKey(ctx context.Context, req *pb.PixRequest) (*pb.PixKeyResponse, error) {
	fmt.Println("recebi req, chave", req.Key)

	var userId = s.keys[req.Key]
	if userId == "" {
		return &pb.PixKeyResponse{Exists: false}, nil
	}
	return &pb.PixKeyResponse{
		Exists: true,
		Active: true,
		UserId: userId,
		Key:    req.Key,
	}, nil
}

func (s *PixGrpcService) CreateKey(ctx context.Context, req *pb.PixCreateKeyRequest) (*pb.PixCreateKeyResponse, error) {
	// vai virar um enum, somente testes por enquanto
	if req.Key == "" && req.Kind == "random" {
		req.Key = uuid.New().String()
	}

	existant := s.keys[req.Key]
	if existant != "" {
		return &pb.PixCreateKeyResponse{}, fmt.Errorf("key already exists")
	}

	s.keys[req.Key] = req.UserId
	return &pb.PixCreateKeyResponse{
		Key: req.Key,
	}, nil
}
