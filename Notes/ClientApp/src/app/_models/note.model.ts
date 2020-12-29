export class Note {
  constructor(
    public id: number,
    public header: string,
    public description: string,
    public isFavorites: boolean,
    public imageData: string,
    public imageName: string,
    public imagePath: string
  ) {
  }
}
