export interface Game {
  ID: number;
  Name: string;
  MinPlayers: number;
  MaxPlayers: number;
  MinTime: number;
  MaxTime: number;
  BGGRating: number;
  URL: string | null;
  Thumbnail: string | null;
  Thumbnail2: string | null;
  MainImage: string | null;
  SalePrice: number;
  ourPrice: number;
}