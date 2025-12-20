package services

import (
	"context"
	"fmt"

	pb "github.com/LXSCA7/pix-ledger/services/pix-key-manager/pb/proto"
	"github.com/google/uuid"
	"github.com/redis/go-redis/v9"
	"google.golang.org/grpc/codes"
	"google.golang.org/grpc/status"
)

type PixGrpcService struct {
	pb.UnimplementedPixServiceServer
	client *redis.Client
}

func NewPixGrpcService() *PixGrpcService {
	return &PixGrpcService{
		client: redis.NewClient(&redis.Options{
			Addr:     "localhost:6379", // mudar pra env
			Password: "",
			DB:       0,
		}),
	}
}

func (s *PixGrpcService) FindKey(ctx context.Context, req *pb.PixRequest) (*pb.PixKeyResponse, error) {
	fmt.Println("recebi req, chave", req.Key)

	userId, err := s.client.Get(ctx, req.Key).Result()
	if err == redis.Nil {
		return &pb.PixKeyResponse{Exists: false}, nil
	} else if err != nil {
		fmt.Printf("redis error %v\n", err)
		return nil, status.Errorf(codes.Internal, "internal error")
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

	success, err := s.client.SetNX(ctx, req.Key, req.UserId, 0).Result()
	if err != nil {
		return nil, status.Errorf(codes.Internal, "erro ao conectar no redis")
	}
	if !success {
		return nil, status.Errorf(codes.AlreadyExists, "pix key '%s' already exists", req.Key)
	}

	return &pb.PixCreateKeyResponse{
		Key: req.Key,
	}, nil
}
