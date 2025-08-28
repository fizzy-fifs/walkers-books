export interface Book {
  id: string;
  title: string;
  author: string;
  isbn: string;
  rating?: number;
  comments?: string[];
  coverImageUrl?: string;
  hasNotes: boolean;
}

export interface CreateBookRequest {
  title: string;
  author: string;
  isbn: string;
  rating?: number;
  comment?: string;
  coverImageUrl?: string;
}

export interface UpdateBookRequest {
  bookId: string;
  rating?: number;
  comment?: string;
}
