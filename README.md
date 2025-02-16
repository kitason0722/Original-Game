# Original Game

## ゲーム制作方針
**ゲームの種類:** シューティングゲーム 

**内容:** 
制限時間内に相手の陣地にダメージを与えて破壊することを競う

制限時間:120秒

## ゲームの勝利条件
**1.** 相手の基地のHPを0にする

**2.** 敵プレイヤーを先に7回撃破する（未実装）

**3.** 制限時間後に1.・2.の条件を満たしていない場合、お互いの基地の残りHPを比較して多い方

## シーン遷移
Title => Game => Result => Title

Title => Rule => Title

## 使用キャラの性能
**自機（標準）**

![Image](https://github.com/user-attachments/assets/9dc2b096-58f0-4057-9c53-e12927d7c117)

HP:10、発射レート:0.5s

**操作方法:** Wキー:前進、Sキー:減速、Aキー:反時計回りに回転、Dキー:時計回りに回転、spaceキー:弾を発射

**自機（進化）（未実装）**

![Image](https://github.com/user-attachments/assets/84400060-efc6-4478-b250-c3f78ea1da8e)

HP:10、発射レート:0.3s

**操作方法:** 自機（標準）と同

**基地**

![Image](https://github.com/user-attachments/assets/e10e0d21-ee4b-4c78-bc1e-1f89c059cdfc)

HP:30、発射レート:2.0s（要調整）、2被弾につき1ダメージを受ける

## その他のオブジェクトの性能
**弾（プレイヤー）**

![Image](https://github.com/user-attachments/assets/f8cf907f-930d-4317-8e53-8c92e735a7e1)

**性能:** ダメージ:1

**弾（基地）**

![Image](https://github.com/user-attachments/assets/919627a5-a595-4c8e-aa6b-8ebaea7377de)

**性能:** ダメージ:1、敵を追尾する

