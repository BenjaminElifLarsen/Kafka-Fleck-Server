<template>
  <div class="posbutton" v-bind:class="color" @click="$emit('click',item);return false;">
    <div class="highlight"></div>
    <div class="item"><span>{{item.name}}</span></div>
    <div class="price"><span>{{item.price}},-</span></div></div>

</template>
<!--<div class="ui column"><div class="ui card item" v-bind:class="color" @click="$emit('click',item)"><span>{{item.name}}<br><br>{{item.price}},-</span></div></div>-->
<script>
export default {
    props : ["item"],
    computed : {
      color : function() {
        switch (this.item.category) {
            case "food": return "red";
            case "beverage": return "green";
            case "drink": return "blue";
            case "misc": return "cyan";
            case "goody": return "yellow";
            default: return "";
        }
      }
    }
}
</script>

<style>
div.posbutton {
  display:inline-block;
  margin-left: auto;
  vertical-align:top;
  margin:0.5%;
  
  border:0px;
  height:20vh;
  cursor:pointer;
  position: relative;
  
  width: 19%;
  color:white;
  border-radius: 5px 5px 5px 5px;
}
div.posbutton div.item {
  font-size: 1.6vw;
  text-align:center;
  line-height:2vw;
  height:20vh;
}
div.posbutton div.item span {
  vertical-align:center;
}
div.posbutton div.price {
  font-size: 1vw;
  font-weight:bold;
  position:absolute;
  bottom:0;
  display:block;
  width:100%;
  background: #0001;
  height:4vh;
  padding-right: 15px;
  padding-left: 15px;
}
div.posbutton div.price span {
  float:right;
}
div.posbutton div.highlight {
  height:50px;
  background: #fff4;
  border-radius: 5px 5px 0px 0px;
}
div.posbutton.red {
  background: #ff0000;
}
div.posbutton.green {
  background: #00cc00;
}
div.posbutton.blue {
  background: #0000ff;
}
div.posbutton.yellow {
  background: #aaaa00;
}
div.posbutton.cyan {
  background: #00aaaa;
}
div.posbutton.cyan div.highlight{
  background: #00cccc;
}
</style>